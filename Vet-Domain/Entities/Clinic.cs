﻿using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations;
using Vet_Domain.Interfaces;


namespace Vet_Domain.Entities
{
    public class Clinic : IId, IHasFileUrl
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

        public required Point Location { get; set; }

        [Unicode(false)]
        public string UrlImage { get; set; }
        public List<ClinicVeterinarian> ClinicVeterinarians { get; set; } = new List<ClinicVeterinarian>();
}
}
