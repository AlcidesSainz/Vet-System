using Vet_Domain.Interfaces;

namespace Vet_Domain.Entities
{
    public class ClinicVeterinarian : IId
    {
        public int Id { get; set; }
        public int ClinicId { get; set; }
        public int VeterinarianId { get; set; }
        public Clinic Clinic { get; set; } = null!;
        public Veterinarian Veterinarian { get; set; } = null!;
    }
}
