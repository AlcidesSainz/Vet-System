﻿using Vet_Domain.Entities;
using Vet_Domain.Interfaces;

namespace Vet_Application.DTOs.Response
{
    public class SpecialtyResponseDTO:IId
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
