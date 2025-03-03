﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Vet_System.Services.Interfaces;

namespace Vet_System.Model.Entities
{
    public class Clinic : IId, IHasFileUrl
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="The field {0} is required")]
        [StringLength(100,ErrorMessage ="The {0} must have {1} characters or less")]
        public required string Name { get; set; }
        [Phone]
        public required string Phone { get; set; }
        [StringLength(100)]
        [EmailAddress]
        public required string Email { get; set; }
        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(100, ErrorMessage = "The {0} must have {1} characters or less")]
        public required string Address { get; set; }
        [Unicode(false)]
        public string? Logo { get; set; }
    }
}
