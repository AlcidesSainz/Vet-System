using Vet_Domain.Entities;
using Vet_Domain.Interfaces;

namespace Vet_Application.DTOs.Response
{
    public class VeterinarianSpecialtyResponseDTO : IId
    {
        public int Id { get; set; }
        public int VeterinarianId { get; set; }
        public int SpecialtyId { get; set; }
    }
}
