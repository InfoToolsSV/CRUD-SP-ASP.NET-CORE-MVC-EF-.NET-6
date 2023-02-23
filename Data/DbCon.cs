using Microsoft.EntityFrameworkCore;
using MVC_EF.Models;

namespace MVC_EF.Data
{
    public class DbCon : DbContext
    {
        public DbCon(DbContextOptions<DbCon> options) : base(options)
        {

        }

        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Usuario>().ToTable("Usuarios");
            builder.Entity<Usuario>().HasKey(u => u.Id_Usuario);
            builder.Entity<Usuario>().Property(u => u.Id_Usuario).HasColumnName("Id_Usuario");
            builder.Entity<Usuario>().Property(u => u.Nombre).HasColumnName("Nombre");
            builder.Entity<Usuario>().Property(u => u.Edad).HasColumnName("Edad");
        }

        public List<Usuario> ObtenerUsuariosSP()
        {
            return Usuarios.FromSqlRaw("exec sp_obtener_usuarios").ToList();
        }

        public Usuario? ObtenerUsuarioPorIdSP(int id)
        {
            var usuario = Usuarios.FromSqlInterpolated($"exec sp_obtener_usuario {id}").AsEnumerable().FirstOrDefault();
            return usuario;
        }

        public void CrearUsuarioSP(string nombre, int edad)
        {
            Database.ExecuteSqlRaw("exec sp_insertar_usuario {0}, {1}", nombre, edad);
        }

        public void ActualizarUsuarioSP(int id, string nombre, int edad)
        {
            Database.ExecuteSqlRaw("exec sp_actualizar_usuario {0}, {1}, {2}", id, nombre, edad);
        }

        public void EliminarUsuarioSP(int id)
        {
            Database.ExecuteSqlRaw("exec sp_eliminar_usuario {0}", id);
        }

    }
}