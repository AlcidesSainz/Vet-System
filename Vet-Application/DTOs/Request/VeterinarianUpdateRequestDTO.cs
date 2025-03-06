using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vet_Application.DTOs.Request
{
    public class VeterinarianUpdateRequestDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }
        [Required]
        [MaxLength(20)]
        public string? LicenseNumber { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [Phone]
        public string? Phone { get; set; }
        public DateTime HireDate { get; set; }
        [Unicode(false)]
        public IFormFile? UrlImage { get; set; }
    }
}
