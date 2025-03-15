using System.ComponentModel.DataAnnotations;

namespace Vet_Application.DTOs.Request
{
    public class OwnerUpdateRequestDTO
    {
        public int Id { get; set; }
        [StringLength(100)]
        [Required]
        public required string Name { get; set; }
        [StringLength(100)]
        public string? Address { get; set; }
        [Phone]
        [Required]
        public required string Phone { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
    }
}
