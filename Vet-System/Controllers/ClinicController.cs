using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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

#pragma warning disable IDE0290 // Use primary constructor
namespace Vet_System.Controllers
{
    [ApiController]
    [Route("api/clinic")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Policy = "IsAdmin")]
    public class ClinicController : CustomBaseController
    {
        private readonly IOutputCacheStore outputCacheStore;
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IMapper mapper;
        private readonly IFileStorage fileStorage;
        private const string cacheTag = "clinic";
        private readonly string container = "clinic";

        public ClinicController(IOutputCacheStore outputCacheStore, ApplicationDbContext applicationDbContext, IMapper mapper, IFileStorage fileStorage)
            : base(applicationDbContext, mapper, outputCacheStore, cacheTag, fileStorage)
        {
            this.outputCacheStore = outputCacheStore;
            this.applicationDbContext = applicationDbContext;
            this.mapper = mapper;
            this.fileStorage = fileStorage;
        }

        [HttpGet]
        [OutputCache(Tags = [cacheTag])]
        public async Task<List<ClinicResponseDTO>> Get([FromQuery] PaginationResponseDTO paginationResponseDTO)
        {
            return await Get<Clinic, ClinicResponseDTO>(paginationResponseDTO, c => c.Id);
        }
        [HttpGet("{id}", Name = "GetClinicById")]
        [OutputCache(Tags = [cacheTag])]
        public async Task<ActionResult<ClinicResponseDTO>> Get(int id)
        {
            return await Get<Clinic, ClinicResponseDTO>(
                id,
                c => c.Include(cv => cv.ClinicVeterinarians)
                .ThenInclude(c => c.Veterinarian)
                );
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] ClinicRequestDTO clinicRequestDTO)
        {
            return await PostWithImage<ClinicRequestDTO, Clinic, ClinicResponseDTO>(clinicRequestDTO, dto => dto.UrlImage, container, "GetClinicById");
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromForm] ClinicUpdateRequestDTO clinicUpdateRequestDTO)
        {
            return await PutWithImage<ClinicUpdateRequestDTO, Clinic>(id, clinicUpdateRequestDTO, dto => dto.UrlImage, container);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await Delete<Clinic>(id);
        }
    }
}
