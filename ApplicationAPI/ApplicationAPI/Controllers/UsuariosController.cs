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
using FluentValidation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly BaseDatosContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<UsuarioDTO> _validator;

        public UsuariosController(BaseDatosContext context, IMapper mapper, IValidator<UsuarioDTO> validator)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDTO>>> GetUsuariosContext()
        {
            if(_context.UsuariosContext.Count() <= 0)
            {
                return NoContent();

            }
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
            var u = await _context.UsuariosContext.FindAsync(id);
            if (u == null) 
            { 
                return BadRequest();
            }

            _mapper.Map(usuario, u);

            try
            {
                await _context.SaveChangesAsync();
                return Ok(usuario);
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
        public async Task<ActionResult<UsuarioDTO>> PostUsuario([FromBody]UsuarioDTO usuario)
        {
            var resultado = await _validator.ValidateAsync(usuario);
            if (!resultado.IsValid) 
            {
                ModelStateDictionary modelState = new ModelStateDictionary();
                foreach (var error in resultado.Errors) modelState.AddModelError(error.PropertyName, error.ErrorMessage);

                return BadRequest(modelState);
            }
            var u = _mapper.Map<Usuario>(usuario);
            _context.UsuariosContext.Add(u);
            await _context.SaveChangesAsync();
            var usuarioDevuelto = _mapper.Map<UsuarioDTO>(u);
            return CreatedAtAction("GetUsuario", new { id = u.Id }, usuarioDevuelto);
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.UsuariosContext.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            _context.UsuariosContext.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioExists(int id)
        {
            return  _context.UsuariosContext.Any(e => e.Id == id);
        }
    }
}
