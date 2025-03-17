using System.ComponentModel.DataAnnotations;
using Vet_Domain.Interfaces;

namespace Vet_Domain.Entities
{
    public class Pet : IId
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        public int BreedId { get; set; }
        public DateTime? BirthDate { get; set; }
        [Range(0,double.MaxValue)]
        public float? Weight { get; set; }
        public int OwnerId { get; set; }
        public Species Species { get; set; } = null!;
        public Breed Breed { get; set; } = null!;
        public Owner Owner { get; set; } = null!;

    }
}
