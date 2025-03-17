using System.ComponentModel.DataAnnotations;
using Vet_Domain.Interfaces;

namespace Vet_Domain.Entities
{
    public class Breed : IId
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int SpeciesId { get; set; }
        public Species Species { get; set; }
        public List<Pet> Pets { get; set; } = new List<Pet>();

    }
}
