using ApplicationAPI.DTO;
using ApplicationAPI.Modelo;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        public List<Usuario> ListaUsuarios { get; set; } = new List<Usuario>
            {
            new Usuario { Id = 1, Name = "Alice", Email = "alice@mail.com", Password = "password123" },
            new Usuario { Id = 2, Name = "Bob", Email = "bob@mail.com", Password = "qwerty456" },
            new Usuario { Id = 3, Name = "Charlie", Email = "charlie@mail.com", Password = "secreto789" }
            };


        // GET: api/<UsuarioController>
        [HttpGet]
        public ActionResult<IEnumerable<Usuario>> Get()
        {
            return Ok(ListaUsuarios);
        }

        // GET api/<UsuarioController>/5
        [HttpGet("{id}")]
        public ActionResult<Usuario> Get(int id)
        {
            if (id <= 0) 
            {
                return BadRequest("El ID introducido no es válido");
            }
            var usuario = ListaUsuarios.FirstOrDefault(u => u.Id == id);
            if (usuario == null) 
            { 
                return NotFound("No se encuentra el usuario");
            }
            return Ok(usuario);
        }

        // POST api/<UsuarioController>
        [HttpPost]
        public IActionResult Post([FromBody] UsuarioDTO usuarioDto)
        {
            if (usuarioDto == null)
            {
                return BadRequest("Datos no válidos");    
            }
            Usuario u = new Usuario();
            u.Email = usuarioDto.Email;
            u.Password = usuarioDto.Password;

            ListaUsuarios.Add(u);
            return Ok(u);
        }

        // PUT api/<UsuarioController>/5
        [HttpPut("{id}")]
        public IActionResult Put([FromHeader]int id, [FromBody] UsuarioDTO usuarioDto)
        {
            if (id <= 0) 
            {
                return BadRequest("Id no válido");
            }

            var usuario = ListaUsuarios.FirstOrDefault(u =>u.Id == id);

            if (usuario == null) 
            {
                return NotFound("Usuario no encontrado");
            }

            if (usuarioDto == null) 
            {
                return BadRequest("Datos no válidos");
            }

            usuario.Email = usuarioDto.Email;
            usuario.Name = usuarioDto.Name;
            usuario.Password = usuarioDto.Password;

            return Ok(usuario);
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute]int id)
        {
            if (id <= 0) 
            { 
                return BadRequest("Id no válido");
            }
            ListaUsuarios.RemoveAt(id);
            return Ok("Usuario eliminado");
        }
    }
}
