using System.ComponentModel.DataAnnotations;
using Vet_Domain.Interfaces;

namespace Vet_Domain.Entities
{
    public class Species:IId
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<Pet> Pets { get; set; } = new List<Pet>();
        public List<Breed> Breeds { get; set; } = new List<Breed>();
    }
}
