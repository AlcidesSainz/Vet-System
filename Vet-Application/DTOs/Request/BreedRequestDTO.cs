using System.ComponentModel.DataAnnotations;
using Vet_Domain.Entities;

namespace Vet_Application.DTOs.Request
{
    public class BreedRequestDTO
    {
        [Required]
        public string? Name { get; set; }
        public int SpeciesId { get; set; }
    }
}
