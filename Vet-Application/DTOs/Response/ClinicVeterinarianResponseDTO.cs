using Vet_Domain.Interfaces;

namespace Vet_Application.DTOs.Response
{
    public class ClinicVeterinarianResponseDTO : IId
    {
        public int Id { get; set; }
        public int ClinicId { get; set; }
        public int VeterinarianId { get; set; }
        public ClinicResponseDTO Clinic { get; set; } = null!;
        public VeterinarianResponseDTO Veterinarian { get; set; } = null!;
    }
}
