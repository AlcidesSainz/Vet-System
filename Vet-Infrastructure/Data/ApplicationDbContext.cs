using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Vet_Domain.Entities;
namespace Vet_Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Clinic
            modelBuilder.Entity<Clinic>()
                 .HasIndex(v => v.Email)
                 .IsUnique();

            modelBuilder.Entity<Clinic>()
                .HasIndex(v => v.Phone)
                .IsUnique();
            #endregion

            #region Veterinarian
            modelBuilder.Entity<Veterinarian>()
                .HasIndex(v => v.Email)
                .IsUnique();

            modelBuilder.Entity<Veterinarian>()
                .HasIndex(v => v.LicenseNumber)
                .IsUnique();
            modelBuilder.Entity<Veterinarian>()
                .HasIndex(v => v.Phone)
                .IsUnique();
            #endregion

            #region ClinicVeterinarian
            modelBuilder.Entity<ClinicVeterinarian>()
                .HasOne(cv => cv.Clinic)
                .WithMany(c => c.ClinicVeterinarians)
                .HasForeignKey(cv => cv.ClinicId);

            modelBuilder.Entity<ClinicVeterinarian>()
                .HasOne(cv => cv.Veterinarian)
                .WithMany(v => v.ClinicVeterinarians)
                .HasForeignKey(cv => cv.VeterinarianId);
            #endregion

        }
        #region Entities
        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<Veterinarian> Veterinarians { get; set; }
        public DbSet<ClinicVeterinarian> ClinicVeterinarians { get; set; }
        #endregion


    }
}
