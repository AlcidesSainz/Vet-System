using System.ComponentModel.DataAnnotations;
using Vet_Domain.Interfaces;

namespace Vet_Application.DTOs.Response
{
    public class OwnerResponseDTO: IId
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Address { get; set; }
        public required string Phone { get; set; }
        public string? Email { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string? IdentificationNumber { get; set; }
        public bool IsDeleted { get; set; }

    }
}
