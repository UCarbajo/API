using ApplicationAPI.DTO;
using FluentValidation;
namespace ApplicationAPI.Validators
{
    public class UsuarioValidator : AbstractValidator<UsuarioDTO>
    {
        public UsuarioValidator() 
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("El nombre no pude estar vacio").MinimumLength(2).MaximumLength(100).WithMessage("La longitud del nombre no es válido");
            RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress().WithMessage("El correo no es válido");
            RuleFor(x => x.Password).NotEmpty().NotNull().MinimumLength(8).WithMessage("Contraseña no válida");
        }
    }
}
