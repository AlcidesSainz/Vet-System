using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
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
    [Route("api/veterinarianSpecialty")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Policy = "IsAdmin")]

    public class VeterinarianSpecialtyController : CustomBaseController
    {
        private readonly IOutputCacheStore outputCacheStore;
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IMapper mapper;
        private readonly IFileStorage fileStorage;
        private const string cacheTag = "specialty";
        private readonly string container = "specialty";

        public VeterinarianSpecialtyController(IOutputCacheStore outputCacheStore, ApplicationDbContext applicationDbContext, IMapper mapper, IFileStorage fileStorage)
            : base(applicationDbContext, mapper, outputCacheStore, cacheTag, fileStorage)
        {
            this.outputCacheStore = outputCacheStore;
            this.applicationDbContext = applicationDbContext;
            this.mapper = mapper;
            this.fileStorage = fileStorage;
        }
        [HttpGet]
        [OutputCache(Tags = [cacheTag])]
        public async Task<List<VeterinarianSpecialtyResponseDTO>> Get([FromQuery] PaginationResponseDTO paginationResponseDTO)
        {
            return await Get<VeterinarianSpecialty, VeterinarianSpecialtyResponseDTO>(paginationResponseDTO, c => c.Id);
        }
        [HttpGet("{id}", Name = "GetVeterinarianSpecialtyById")]
        [OutputCache(Tags = [cacheTag])]
        public async Task<ActionResult<VeterinarianSpecialtyResponseDTO>> Get(int id)
        {
            return await Get<VeterinarianSpecialty, VeterinarianSpecialtyResponseDTO>(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] VeterinarianSpecialtyRequestDTO veterinarianSpecialtyRequestDTO)
        {
            return await Post<VeterinarianSpecialtyRequestDTO, VeterinarianSpecialty, VeterinarianSpecialtyResponseDTO>(veterinarianSpecialtyRequestDTO, "GetVeterinarianSpecialtyById");
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] VeterinarianSpecialtyUpdateRequestDTO veterinarianSpecialtyUpdateRequestDTO)
        {
            return await Put<VeterinarianSpecialtyUpdateRequestDTO, VeterinarianSpecialty>(id, veterinarianSpecialtyUpdateRequestDTO);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await Delete<VeterinarianSpecialty>(id);
        }
    }
}
