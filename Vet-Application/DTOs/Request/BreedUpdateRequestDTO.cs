using System.ComponentModel.DataAnnotations;

namespace Vet_Application.DTOs.Request
{
    public class BreedUpdateRequestDTO
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public int SpeciesId { get; set; }
    }
}
