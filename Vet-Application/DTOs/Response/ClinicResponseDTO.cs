using Vet_Domain.Interfaces;
using Vet_System.Services.Interfaces;

namespace Vet_Application.DTOs.Response
{
    public class ClinicResponseDTO : IId, IHasFileUrl
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Phone { get; set; }
        public required string Email { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public string? UrlImage { get; set; }
        public List<VeterinarianResponseDTO> Veterinarians { get; set; } = new();
    }
}
