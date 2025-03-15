using Vet_Domain.Interfaces;

namespace Vet_Domain.Entities
{
    public class Specialty : IId
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<VeterinarianSpecialty> VeterinarianSpecialties { get; set; } = new List<VeterinarianSpecialty>();
    }
}
