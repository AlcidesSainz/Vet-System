using System.ComponentModel.DataAnnotations;
using Vet_Domain.Entities;

namespace Vet_Application.DTOs.Request
{
    public class SpeciesUpdateRequestDTO
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
    }
}
