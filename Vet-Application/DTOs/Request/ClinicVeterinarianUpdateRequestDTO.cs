using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vet_Application.DTOs.Request
{
    public class ClinicVeterinarianUpdateRequestDTO
    {
        public int ClinicId { get; set; }
        public int VeterinarianId { get; set; }
    }
}
