using System.ComponentModel.DataAnnotations;
using Vet_System.Services.Interfaces;

namespace Vet_System.Services.DTOs.Response
{
    public class ClinicResponseDTO : IId, IHasFileUrl
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Phone { get; set; }
        public required string Email { get; set; }
        public required string Address { get; set; }
        public string? Logo { get; set; }
    }
}
