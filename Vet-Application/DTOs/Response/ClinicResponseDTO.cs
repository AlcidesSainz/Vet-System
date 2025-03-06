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
        public required string Address { get; set; }
        public string? UrlLogo { get; set; }
    }
}
