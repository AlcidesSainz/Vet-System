using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Vet_Application.DTOs.Request;
using Vet_Application.DTOs.Response;
using Vet_Domain.Entities;
using Vet_Infrastructure.Data;
using Vet_Infrastructure.Services.Interfaces;
using Vet_System.Controllers.Base;
using Vet_System.Services.DTOs.Response;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Vet_System.Controllers
{
    [Route("api/veterinarian")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]

    public class VeterinarianController : CustomBaseController
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IMapper mapper;
        private readonly IOutputCacheStore outputCacheStore;
        private const string cacheTag = "veterinarian";
        private readonly string container = "veterinarian";

        public VeterinarianController(ApplicationDbContext applicationDbContext, IMapper mapper, IOutputCacheStore outputCacheStore, IFileStorage fileStorage)
            : base(applicationDbContext, mapper, outputCacheStore, cacheTag, fileStorage)
        {
            this.applicationDbContext = applicationDbContext;
            this.mapper = mapper;
            this.outputCacheStore = outputCacheStore;
        }
        [HttpGet]
        [OutputCache(Tags = [cacheTag])]
        public async Task<List<VeterinarianResponseDTO>> Get([FromQuery] PaginationResponseDTO paginationResponseDTO)
        {
            return await Get<Veterinarian, VeterinarianResponseDTO>(paginationResponseDTO, c => c.Id);
        }
        [HttpGet("{id}", Name = "GetVeterinarianById")]
        [OutputCache(Tags = [cacheTag])]
        public async Task<ActionResult<VeterinarianResponseDTO>> Get(int id)
        {
            return await Get<Veterinarian, VeterinarianResponseDTO>(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] VeterinarianRequestDTO veterinarianRequestDTO)
        {
            return await PostWithImage<VeterinarianRequestDTO, Veterinarian, VeterinarianResponseDTO>(veterinarianRequestDTO, dto => dto.UrlImage, container, "GetVeterinarianById");
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromForm] VeterinarianUpdateRequestDTO veterinarianUpdateRequestDTO)
        {
            return await PutWithImage<VeterinarianUpdateRequestDTO, Veterinarian>(id, veterinarianUpdateRequestDTO, dto => dto.UrlImage, container);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await Delete<Veterinarian>(id);
        }
    }
}
