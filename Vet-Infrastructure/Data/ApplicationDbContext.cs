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

            #region Owner
            modelBuilder.Entity<Owner>()
                .HasIndex(v => v.Email)
                .IsUnique();
            modelBuilder.Entity<Owner>()
                .HasIndex(v => v.Phone)
                .IsUnique();
            modelBuilder.Entity<Owner>()
                .HasIndex(v => v.Name)
                .IsUnique();
            modelBuilder.Entity<Owner>()
                .HasIndex(v => v.IdentificationNumber)
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

            #region Species
            modelBuilder.Entity<Species>()
                .HasIndex(s => s.Name)
                .IsUnique();
            modelBuilder.Entity<Species>().HasData(
                new Species { Id = 1, Name = "Perro" },
                new Species { Id = 2, Name = "Gato" },
                new Species { Id = 3, Name = "Ave" },
                new Species { Id = 4, Name = "Conejo" },
                new Species { Id = 5, Name = "Hámster" },
                new Species { Id = 6, Name = "Pez" }
                );
            #endregion

            #region Breed
            modelBuilder.Entity<Breed>()
                .HasIndex(b => b.Name )
                .IsUnique();
            modelBuilder.Entity<Breed>()
                .HasOne(b => b.Species)
                .WithMany(s => s.Breeds)
                .HasForeignKey(b => b.SpeciesId);

            modelBuilder.Entity<Breed>().HasData(
                // -----------------------------------------
                //  Perros (SpeciesId = 1)
                // -----------------------------------------
                new Breed { Id = 1, Name = "Labrador Retriever", SpeciesId = 1 },
                new Breed { Id = 2, Name = "Bulldog", SpeciesId = 1 },
                new Breed { Id = 3, Name = "Pastor Alemán", SpeciesId = 1 },
                new Breed { Id = 4, Name = "Golden Retriever", SpeciesId = 1 },
                new Breed { Id = 5, Name = "Poodle", SpeciesId = 1 },
                new Breed { Id = 6, Name = "Chihuahua", SpeciesId = 1 },
                new Breed { Id = 7, Name = "Rottweiler", SpeciesId = 1 },
                new Breed { Id = 8, Name = "Yorkshire Terrier", SpeciesId = 1 },
                new Breed { Id = 9, Name = "Boxer", SpeciesId = 1 },
                new Breed { Id = 10, Name = "Dachshund", SpeciesId = 1 },
                new Breed { Id = 11, Name = "Shih Tzu", SpeciesId = 1 },
                new Breed { Id = 12, Name = "Schnauzer", SpeciesId = 1 },
                new Breed { Id = 13, Name = "Pug", SpeciesId = 1 },
                new Breed { Id = 14, Name = "Doberman", SpeciesId = 1 },
                new Breed { Id = 15, Name = "Beagle", SpeciesId = 1 },
                new Breed { Id = 16, Name = "Border Collie", SpeciesId = 1 },
                new Breed { Id = 17, Name = "Bichón Frisé", SpeciesId = 1 },
                new Breed { Id = 18, Name = "Husky Siberiano", SpeciesId = 1 },
                new Breed { Id = 19, Name = "Pastor Belga", SpeciesId = 1 },
                new Breed { Id = 20, Name = "Maltés", SpeciesId = 1 },
                // -----------------------------------------
                //   Gatos (SpeciesId = 2)
                // -----------------------------------------
                new Breed { Id = 21, Name = "Siamés", SpeciesId = 2 },
                new Breed { Id = 22, Name = "Persa", SpeciesId = 2 },
                new Breed { Id = 23, Name = "Maine Coon", SpeciesId = 2 },
                new Breed { Id = 24, Name = "Bengalí", SpeciesId = 2 },
                new Breed { Id = 25, Name = "Sphynx", SpeciesId = 2 },
                new Breed { Id = 26, Name = "British Shorthair", SpeciesId = 2 },
                new Breed { Id = 27, Name = "Scottish Fold", SpeciesId = 2 },
                new Breed { Id = 28, Name = "Ragdoll", SpeciesId = 2 },
                new Breed { Id = 29, Name = "American Shorthair", SpeciesId = 2 },
                new Breed { Id = 30, Name = "Exótico de Pelo Corto", SpeciesId = 2 },
                new Breed { Id = 31, Name = "Azul Ruso", SpeciesId = 2 },
                new Breed { Id = 32, Name = "Birmano", SpeciesId = 2 },
                new Breed { Id = 33, Name = "Himalayo", SpeciesId = 2 },
                new Breed { Id = 34, Name = "Angora Turco", SpeciesId = 2 },
                new Breed { Id = 35, Name = "Abisinio", SpeciesId = 2 },
                new Breed { Id = 36, Name = "Ocicat", SpeciesId = 2 },
                new Breed { Id = 37, Name = "Bombay", SpeciesId = 2 },
                new Breed { Id = 38, Name = "Chartreux", SpeciesId = 2 },
                new Breed { Id = 39, Name = "Devon Rex", SpeciesId = 2 },
                new Breed { Id = 40, Name = "Cornish Rex", SpeciesId = 2 },
                // -----------------------------------------
                //   Aves (SpeciesId = 3)
                // -----------------------------------------
                new Breed { Id = 41, Name = "Periquito", SpeciesId = 3 },
                new Breed { Id = 42, Name = "Canario", SpeciesId = 3 },
                new Breed { Id = 43, Name = "Agapornis (Inseparable)", SpeciesId = 3 },
                new Breed { Id = 44, Name = "Ninfa (Cockatiel)", SpeciesId = 3 },
                new Breed { Id = 45, Name = "Lorito Australiano", SpeciesId = 3 },
                new Breed { Id = 46, Name = "Guacamayo", SpeciesId = 3 },
                new Breed { Id = 47, Name = "Loro Amazónico", SpeciesId = 3 },
                new Breed { Id = 48, Name = "Cacatúa", SpeciesId = 3 },
                new Breed { Id = 49, Name = "Periquito de Bourke", SpeciesId = 3 },
                new Breed { Id = 50, Name = "Diamante Mandarín", SpeciesId = 3 },
                new Breed { Id = 51, Name = "Diamante Gould", SpeciesId = 3 },
                new Breed { Id = 52, Name = "Cotorra", SpeciesId = 3 },
                new Breed { Id = 53, Name = "Yaco (Loro Gris Africano)", SpeciesId = 3 },
                new Breed { Id = 54, Name = "Loro Eclectus", SpeciesId = 3 },
                new Breed { Id = 55, Name = "Periquito de Línea", SpeciesId = 3 },
                new Breed { Id = 56, Name = "Conure del Sol", SpeciesId = 3 },
                new Breed { Id = 57, Name = "Conure Cabeza Negra", SpeciesId = 3 },
                new Breed { Id = 58, Name = "Agapornis Fischer", SpeciesId = 3 },
                new Breed { Id = 59, Name = "Agapornis Roseicollis", SpeciesId = 3 },
                new Breed { Id = 60, Name = "Periquito Ondulado", SpeciesId = 3 },
                // -----------------------------------------
                //    Conejo (SpeciesId = 4) -> Id 61..80
                // -----------------------------------------
                new Breed { Id = 61, Name = "Holland Lop", SpeciesId = 4 },
                new Breed { Id = 62, Name = "Netherland Dwarf", SpeciesId = 4 },
                new Breed { Id = 63, Name = "Rex", SpeciesId = 4 },
                new Breed { Id = 64, Name = "Cabeza de León", SpeciesId = 4 },
                new Breed { Id = 65, Name = "Belier Francés", SpeciesId = 4 },
                new Breed { Id = 66, Name = "Californiano", SpeciesId = 4 },
                new Breed { Id = 67, Name = "Dutch Rabbit", SpeciesId = 4 },
                new Breed { Id = 68, Name = "Flemish Giant", SpeciesId = 4 },
                new Breed { Id = 69, Name = "Hotot Enano", SpeciesId = 4 },
                new Breed { Id = 70, Name = "Mini Lop", SpeciesId = 4 },
                new Breed { Id = 71, Name = "American Fuzzy Lop", SpeciesId = 4 },
                new Breed { Id = 72, Name = "English Angora", SpeciesId = 4 },
                new Breed { Id = 73, Name = "English Spot", SpeciesId = 4 },
                new Breed { Id = 74, Name = "Harlequin", SpeciesId = 4 },
                new Breed { Id = 75, Name = "Tan Rabbit", SpeciesId = 4 },
                new Breed { Id = 76, Name = "Satin Rabbit", SpeciesId = 4 },
                new Breed { Id = 77, Name = "Cinnamon Rabbit", SpeciesId = 4 },
                new Breed { Id = 78, Name = "Chinchilla Rabbit", SpeciesId = 4 },
                new Breed { Id = 79, Name = "Plata de Champagne", SpeciesId = 4 },
                new Breed { Id = 80, Name = "Gigante de España", SpeciesId = 4 },
                // -----------------------------------------
                //    Hámster (SpeciesId = 5)
                // -----------------------------------------
                new Breed { Id = 81, Name = "Hámster Sirio Dorado", SpeciesId = 5 },
                new Breed { Id = 82, Name = "Hámster Sirio Panda", SpeciesId = 5 },
                new Breed { Id = 83, Name = "Hámster Sirio de Pelo Largo", SpeciesId = 5 },
                new Breed { Id = 84, Name = "Hámster Enano Ruso (Winter White)", SpeciesId = 5 },
                new Breed { Id = 85, Name = "Hámster Enano de Campbell", SpeciesId = 5 },
                new Breed { Id = 86, Name = "Hámster Enano Roborowski", SpeciesId = 5 },
                new Breed { Id = 87, Name = "Hámster Chino", SpeciesId = 5 },
                new Breed { Id = 88, Name = "Hámster Sirio Satinado", SpeciesId = 5 },
                new Breed { Id = 89, Name = "Hámster Sirio Bicolor", SpeciesId = 5 },
                new Breed { Id = 90, Name = "Hámster Sirio Black Bear", SpeciesId = 5 },
                new Breed { Id = 91, Name = "Hámster Sirio Tortoiseshell", SpeciesId = 5 },
                new Breed { Id = 92, Name = "Hámster Sirio Dalmatian", SpeciesId = 5 },
                new Breed { Id = 93, Name = "Hámster Sirio Calico", SpeciesId = 5 },
                new Breed { Id = 94, Name = "Hámster Sirio Chocolate", SpeciesId = 5 },
                new Breed { Id = 95, Name = "Hámster Sirio Cream", SpeciesId = 5 },
                new Breed { Id = 96, Name = "Hámster Sirio Albino", SpeciesId = 5 },
                new Breed { Id = 97, Name = "Hámster Enano Campbell Albino", SpeciesId = 5 },
                new Breed { Id = 98, Name = "Hámster Enano Ruso Zafiro", SpeciesId = 5 },
                new Breed { Id = 99, Name = "Hámster Enano Ruso Perla", SpeciesId = 5 },
                new Breed { Id = 100, Name = "Hámster Chino Rayado", SpeciesId = 5 },
                // -----------------------------------------
                //    Pez (SpeciesId = 6)
                // -----------------------------------------
                new Breed { Id = 101, Name = "Betta", SpeciesId = 6 },
                new Breed { Id = 102, Name = "Guppy", SpeciesId = 6 },
                new Breed { Id = 103, Name = "Molly", SpeciesId = 6 },
                new Breed { Id = 104, Name = "Platy", SpeciesId = 6 },
                new Breed { Id = 105, Name = "Tetra Neón", SpeciesId = 6 },
                new Breed { Id = 106, Name = "Tetra Cardenal", SpeciesId = 6 },
                new Breed { Id = 107, Name = "Pez Ángel (Escalar)", SpeciesId = 6 },
                new Breed { Id = 108, Name = "Disco", SpeciesId = 6 },
                new Breed { Id = 109, Name = "Goldfish", SpeciesId = 6 },
                new Breed { Id = 110, Name = "Carpa Koi", SpeciesId = 6 },
                new Breed { Id = 111, Name = "Cíclido Óscar", SpeciesId = 6 },
                new Breed { Id = 112, Name = "Corydora", SpeciesId = 6 },
                new Breed { Id = 113, Name = "Locha Payaso", SpeciesId = 6 },
                new Breed { Id = 114, Name = "Gourami Enano", SpeciesId = 6 },
                new Breed { Id = 115, Name = "Pez Arcoíris Boesemani", SpeciesId = 6 },
                new Breed { Id = 116, Name = "Barbo Tigre", SpeciesId = 6 },
                new Breed { Id = 117, Name = "Pez Payaso (Amphiprion ocellaris)", SpeciesId = 6 },
                new Breed { Id = 118, Name = "Pez Globo Enano", SpeciesId = 6 },
                new Breed { Id = 119, Name = "Killifish", SpeciesId = 6 },
                new Breed { Id = 120, Name = "Pez Gato Plecostomus", SpeciesId = 6 }
                );
            #endregion

            #region Pet
            modelBuilder.Entity<Pet>()
                .HasOne(p=>p.Breed)
                .WithMany(b => b.Pets)
                .HasForeignKey(p => p.BreedId);
            modelBuilder.Entity<Pet>()
                .HasOne(p => p.Owner)
                .WithMany(o => o.Pets)
                .HasForeignKey(p => p.OwnerId);
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
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Species> Species { get; set; }
        public DbSet<Breed> Breeds { get; set; }

        #endregion


    }
}
