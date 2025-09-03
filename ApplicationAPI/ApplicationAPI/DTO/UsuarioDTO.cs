using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace ApplicationAPI.DTO
{
    public class UsuarioDTO
    {
        public string? Name {  get; set; }
        public string? Email {  get; set; }
        public string? Password { get; set; }
    }
}
