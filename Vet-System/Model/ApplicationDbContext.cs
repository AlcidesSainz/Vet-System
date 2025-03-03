using Microsoft.EntityFrameworkCore;
using Vet_System.Model.Entities;

namespace Vet_System.Model
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Clinic> Clinics { get; set; }
    }
}
