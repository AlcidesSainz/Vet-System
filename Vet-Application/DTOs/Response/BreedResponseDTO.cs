using System.ComponentModel.DataAnnotations;
using Vet_Domain.Entities;
using Vet_Domain.Interfaces;

namespace Vet_Application.DTOs.Response
{
    public class BreedResponseDTO:IId
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int SpeciesId { get; set; }
    }
}
