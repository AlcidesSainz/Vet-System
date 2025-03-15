using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Vet_Domain.Entities;
namespace Vet_Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        private readonly DbContextOptions options;

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            this.options = options;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

            #region Specialty
            var specialties = new[]
            {
                  new Specialty { Id = 1, Name = "General" },
            new Specialty { Id = 2, Name = "Medicina interna" },
            new Specialty { Id = 3, Name = "Cirugía" },
            new Specialty { Id = 4, Name = "Dermatología" },
            new Specialty { Id = 5, Name = "Oftalmología" },
            new Specialty { Id = 6, Name = "Oncología" },
            new Specialty { Id = 7, Name = "Cardiología" },
            new Specialty { Id = 8, Name = "Neurología" },
            new Specialty { Id = 9, Name = "Odontología" },
            new Specialty { Id = 10, Name = "Nutrición" },
            new Specialty { Id = 11, Name = "Rehabilitación" },
            new Specialty { Id = 12, Name = "Comportamiento" },
            new Specialty { Id = 13, Name = "Anestesiología" },
            new Specialty { Id = 14, Name = "Radiología" },
            new Specialty { Id = 15, Name = "Urgencias" },
            new Specialty { Id = 16, Name = "Patología" },
            new Specialty { Id = 17, Name = "Laboratorio" },
            new Specialty { Id = 18, Name = "Zootecnia" },
            new Specialty { Id = 19, Name = "Acupuntura" },
            new Specialty { Id = 20, Name = "Homeopatía" },
            new Specialty { Id = 21, Name = "Fisioterapia" },
            new Specialty { Id = 22, Name = "Ortopedia" },
            new Specialty { Id = 23, Name = "Endocrinología" },
            new Specialty { Id = 24, Name = "Geriatria" }
            };
            modelBuilder.Entity<Specialty>().HasData(specialties);

            modelBuilder.Entity<Specialty>()
                .HasIndex(s => s.Name)
                .IsUnique();
            #endregion


            #region VeterinarianSpecialty
            modelBuilder.Entity<VeterinarianSpecialty>()
                .HasOne(cv => cv.Veterinarian)
                .WithMany(c => c.VeterinarianSpecialties)
                .HasForeignKey(cv => cv.VeterinarianId);

            modelBuilder.Entity<VeterinarianSpecialty>()
                .HasOne(cv => cv.Specialty)
                .WithMany(v => v.VeterinarianSpecialties)
                .HasForeignKey(cv => cv.SpecialtyId);
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



            //END
            base.OnModelCreating(modelBuilder);


        }
        #region Entities
        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<Veterinarian> Veterinarians { get; set; }
        public DbSet<ClinicVeterinarian> ClinicVeterinarians { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<VeterinarianSpecialty> VeterinarianSpecialties { get; set; }

        #endregion


    }
}
