using Microsoft.EntityFrameworkCore;
using WebApiTupac.Entities;

namespace WebApiTupac.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Carrera> Carreras { get; set; }
        public DbSet<Materia> Materias { get; set; }
        public DbSet<Cursada> Cursadas { get; set; }
    }
}
