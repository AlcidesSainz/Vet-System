using System.ComponentModel.DataAnnotations;
using Vet_Domain.Interfaces;

namespace Vet_Domain.Entities
{
    public class Owner : IId
    {
        public int Id { get; set; }
        [StringLength(100)]
        [Required]
        public required string Name { get; set; }
        [StringLength(100)]
        public string Address { get; set; }
        [Phone]
        [Required]
        public required string Phone { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }
        
    }
}
