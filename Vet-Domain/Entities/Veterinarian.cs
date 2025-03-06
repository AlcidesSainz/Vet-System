using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Vet_Domain.Interfaces;
using Vet_System.Services.Interfaces;

namespace Vet_Domain.Entities
{
    public class Veterinarian : IId, IHasFileUrl
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(20)]
        public string LicenseNumber { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string Phone { get; set; }
        public DateTime HireDate { get; set; }
        [Unicode(false)]
        public string UrlImage { get; set; }
        public List<ClinicVeterinarian> ClinicVeterinarians { get; set; } = new();

    }
}
