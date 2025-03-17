using System.ComponentModel.DataAnnotations;
using Vet_Domain.Entities;
using Vet_Domain.Interfaces;

namespace Vet_Application.DTOs.Response
{
    public class PetResponseDTO:IId
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? OwnerName { get; set; }
        public string? SpeciesName { get; set; }
        public string? BreedName { get; set; }
        public DateTime? BirthDate { get; set; }
        public float? Weight { get; set; }

    }
}
