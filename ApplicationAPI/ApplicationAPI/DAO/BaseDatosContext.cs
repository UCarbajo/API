using ApplicationAPI.Modelo;
using Microsoft.EntityFrameworkCore;

namespace ApplicationAPI.DAO
{
    public class BaseDatosContext : DbContext
    {
        public BaseDatosContext(DbContextOptions<BaseDatosContext> options) : base(options)
        {

        }

        public DbSet<Usuario> UsuariosContext { get; set; } = null!;
        public DbSet<Producto> ProductoContext { get; set; } = null!;
    }
}
