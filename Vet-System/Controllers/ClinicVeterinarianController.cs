using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;
using Vet_Application.DTOs.Request;
using Vet_Application.DTOs.Response;
using Vet_Domain.Entities;
using Vet_Infrastructure.Data;
using Vet_Infrastructure.Services.Interfaces;
using Vet_System.Controllers.Base;
using Vet_System.Services.DTOs.Response;

namespace Vet_System.Controllers
{
    [ApiController]
    [Route("api/clinicVeterinarian")]
    public class ClinicVeterinarianController : CustomBaseController
    {
        private readonly IOutputCacheStore outputCacheStore;
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IMapper mapper;
        private readonly IFileStorage fileStorage;
        private const string cacheTag = "clinicVeterinarian";
        private readonly string container = "clinicVeterinarian";

        public ClinicVeterinarianController(IOutputCacheStore outputCacheStore, ApplicationDbContext applicationDbContext, IMapper mapper, IFileStorage fileStorage)
            : base(applicationDbContext, mapper, outputCacheStore, cacheTag, fileStorage)
        {
            this.outputCacheStore = outputCacheStore;
            this.applicationDbContext = applicationDbContext;
            this.mapper = mapper;
            this.fileStorage = fileStorage;
        }
        [HttpGet]
        [OutputCache(Tags = [cacheTag])]
        public async Task<List<ClinicVeterinarianResponseDTO>> Get([FromQuery] PaginationResponseDTO paginationResponseDTO)
        {
            return await Get<ClinicVeterinarian, ClinicVeterinarianResponseDTO>(paginationResponseDTO, c => c.Id);
        }
        [HttpGet("{id}", Name = "GetClinicVeterinarianById")]
        [OutputCache(Tags = [cacheTag])]
        public async Task<ActionResult<ClinicVeterinarianResponseDTO>> Get(int id)
        {
            return await Get<ClinicVeterinarian, ClinicVeterinarianResponseDTO>(
                id,
                query => query
                .Include(cv => cv.Clinic)
                .Include(cv => cv.Veterinarian)
                );
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ClinicVeterinarianRequestDTO clinicVeterinarianRequestDTO)
        {
            return await Post<ClinicVeterinarianRequestDTO, ClinicVeterinarian, ClinicVeterinarianResponseDTO>
                (clinicVeterinarianRequestDTO, "GetClinicVeterinarianById");
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] ClinicVeterinarianUpdateRequestDTO clinicVeterinarianUpdateRequestDTO)
        {
            return await Put<ClinicVeterinarianUpdateRequestDTO, ClinicVeterinarian>(id, clinicVeterinarianUpdateRequestDTO);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await Delete<ClinicVeterinarian>(id);
        }
    }
}
