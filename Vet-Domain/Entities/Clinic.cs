using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations;
using Vet_Domain.Interfaces;
using Vet_System.Services.Interfaces;

namespace Vet_Domain.Entities
{
    public class Clinic : IId, IHasFileUrl
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(100, ErrorMessage = "The {0} must have {1} characters or less")]
        public required string Name { get; set; }
        [Phone]
        public required string Phone { get; set; }
        [StringLength(100)]
        [EmailAddress]
        public required string Email { get; set; }
       
        public required Point Location { get; set; }

        [Unicode(false)]
        public string UrlLogo { get; set; }
    }
}
