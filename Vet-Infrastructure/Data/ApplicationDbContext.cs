using Microsoft.EntityFrameworkCore;
using Vet_Domain.Entities;
namespace Vet_Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Clinic> Clinics { get; set; }
    }
}
