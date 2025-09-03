using System.ComponentModel.DataAnnotations;

namespace ApplicationAPI.DTO
{
    public class UsuarioDTO
    {
        [Required]
        [StringLength(maximumLength: 1000, MinimumLength = 2, ErrorMessage = "Error en la longitud del nombre")]
        [RegularExpression(@".*\S.*", ErrorMessage = "El nombre no puede estar vacía o solo con espacios")]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get ; set; }
        [Required]
        [StringLength(maximumLength: 1000, MinimumLength = 6, ErrorMessage = "Error en la longitud de la contraseña")]
        [RegularExpression(@".*\S.*", ErrorMessage = "La contraseña no puede estar vacía o solo con espacios")]
        public string Password { get ; set; }
    }
}
