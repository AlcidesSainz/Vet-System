using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Vet_Application.DTOs.Request
{
    public class ClinicUpdateRequestDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public required string Name { get; set; }
        [Phone]
        public required string Phone { get; set; }
        [StringLength(100)]
        [EmailAddress]
        public required string Email { get; set; }
        [Range(-90, 90)]
        public double Lat { get; set; }
        [Range(-180, 180)]
        public double Lng { get; set; }
        public IFormFile? UrlImage { get; set; }
    }
}
