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
    [Route("api/pet")]
    public class PetController : CustomBaseController
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IMapper mapper;
        private readonly IOutputCacheStore outputCacheStore;
        private readonly IFileStorage fileStorage;
        private const string cacheTag = "pet";
        private readonly string container = "pet";

        public PetController(ApplicationDbContext applicationDbContext, IMapper mapper, IOutputCacheStore outputCacheStore, IFileStorage fileStorage)
            : base(applicationDbContext, mapper, outputCacheStore, cacheTag, fileStorage)
        {
            this.applicationDbContext = applicationDbContext;
            this.mapper = mapper;
            this.outputCacheStore = outputCacheStore;
            this.fileStorage = fileStorage;
        }
        [HttpGet]
        [OutputCache(Tags = [cacheTag])]
        public async Task<List<PetResponseDTO>> Get([FromQuery] PaginationResponseDTO paginationResponseDTO)
        {
            return await Get<Pet, PetResponseDTO>(paginationResponseDTO, c => c.Id);
        }
        [HttpGet("{id}", Name = "GetPetById")]
        [OutputCache(Tags = [cacheTag])]
        public async Task<ActionResult<PetResponseDTO>> Get(int id)
        {
            return await Get<Pet, PetResponseDTO>(
                id
                );
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PetRequestDTO petRequestDTO)
        {
            return await Post<PetRequestDTO, Pet, PetResponseDTO>(petRequestDTO, "GetPetById");
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] PetUpdateRequestDTO petUpdateRequestDTO)
        {
            return await Put<PetUpdateRequestDTO, Pet>(id, petUpdateRequestDTO);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await Delete<Pet>(id);
        }
    }
}
