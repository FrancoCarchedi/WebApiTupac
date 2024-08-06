using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApiTupac.Entities;
using WebApiTupac.Services;

namespace WebApiTupac.Data
{
    public class ApplicationDbContext : IdentityDbContext<Usuario, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relacion explicita de muchos a muchos entre Usuarios y Materias, a traves de Cursadas
            //modelBuilder.Entity<Cursada>()
            //    .HasKey(cursada => new { cursada.UsuarioId, cursada.MateriaId });

            modelBuilder.Entity<Cursada>()
            .HasOne(c => c.Usuario)
            .WithMany(u => u.CursadasComoAlumno)
            .HasForeignKey(c => c.UsuarioId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cursada>()
                .HasOne(m => m.Materia)
                .WithMany(u => u.Cursadas)
                .HasForeignKey(c => c.MateriaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cursada>()
            .HasOne(c => c.Docente)
            .WithMany(u => u.CursadasComoDocente)
            .HasForeignKey(c => c.DocenteId)
            .OnDelete(DeleteBehavior.Restrict);

        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Carrera> Carreras { get; set; }
        public DbSet<Materia> Materias { get; set; }
        public DbSet<Cursada> Cursadas { get; set; }
    }
}
