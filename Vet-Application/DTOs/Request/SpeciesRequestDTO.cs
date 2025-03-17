using System.ComponentModel.DataAnnotations;
using Vet_Domain.Entities;

namespace Vet_Application.DTOs.Request
{
    public class SpeciesRequestDTO
    {
        [Required]
        public string? Name { get; set; }
    }
}
