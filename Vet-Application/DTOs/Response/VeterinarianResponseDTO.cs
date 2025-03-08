using Vet_Domain.Interfaces;

namespace Vet_Application.DTOs.Response
{
    public class VeterinarianResponseDTO : IId, IHasFileUrl
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? LicenseNumber { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public DateTime HireDate { get; set; }
        public string? UrlImage { get; set; }
    }
}
