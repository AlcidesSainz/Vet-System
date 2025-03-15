using Vet_Domain.Interfaces;

namespace Vet_Domain.Entities
{
    public class VeterinarianSpecialty : IId
    {
        public int Id { get; set; }
        public int VeterinarianId { get; set; }
        public int SpecialtyId { get; set; }
        public Veterinarian Veterinarian { get; set; }
        public Specialty Specialty { get; set; }

    }
}
