using System.ComponentModel.DataAnnotations;
using Vet_Domain.Entities;

namespace Vet_Application.DTOs.Request
{
    public class PetUpdateRequestDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string? Name { get; set; }
        [Required]
        public int BreedId { get; set; }
        public DateTime? BirthDate { get; set; }
        [Range(0, double.MaxValue)]
        public float? Weight { get; set; }
        public int OwnerId { get; set; }
    }
}
