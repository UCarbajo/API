using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApplicationAPI.DAO;
using ApplicationAPI.Modelo;
using AutoMapper;
using ApplicationAPI.Profiles;
using ApplicationAPI.DTO;
using AutoMapper.QueryableExtensions;

namespace ApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly BaseDatosContext _context;
        private readonly IMapper _mapper;

        public UsuariosController(BaseDatosContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDTO>>> GetUsuariosContext()
        {
            return await _context.UsuariosContext
                .ProjectTo<UsuarioDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDTO>> GetUsuario(int id)
        {
            var usuario = await _context.UsuariosContext
                .Where(u => u.Id == id)
                .ProjectTo<UsuarioDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        // PUT: api/Usuarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, UsuarioDTO usuario)
        {
            if (!UsuarioExists(id))
            {
                return NotFound();
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Usuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            _context.UsuariosContext.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuario", new { id = usuario.Id }, usuario);
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.UsuariosContext
                .Where(u => u.Id == id)
                .ProjectTo<UsuarioDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (usuario == null)
            {
                return NotFound();
            }

            _context.UsuariosContext.Remove(_mapper.Map<Usuario>(usuario));
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioExists(int id)
        {
            return _context.UsuariosContext.Any(e => e.Id == id);
        }
    }
}
