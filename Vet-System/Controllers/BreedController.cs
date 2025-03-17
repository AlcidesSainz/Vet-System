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
    [Route("api/breed")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Policy = "IsAdmin")]
    public class BreedController : CustomBaseController
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IMapper mapper;
        private readonly IOutputCacheStore outputCacheStore;
        private readonly IFileStorage fileStorage;
        private const string cacheTag = "breed";
        private readonly string container = "breed";

        public BreedController(ApplicationDbContext applicationDbContext,IMapper mapper,IOutputCacheStore outputCacheStore,IFileStorage fileStorage)
            : base(applicationDbContext, mapper, outputCacheStore, cacheTag, fileStorage)
        {
            this.applicationDbContext = applicationDbContext;
            this.mapper = mapper;
            this.outputCacheStore = outputCacheStore;
            this.fileStorage = fileStorage;
        }
        [HttpGet]
        [OutputCache(Tags = [cacheTag])]
        public async Task<List<BreedResponseDTO>> Get([FromQuery] PaginationResponseDTO paginationResponseDTO)
        {
            return await Get<Breed, BreedResponseDTO>(paginationResponseDTO, c => c.Id);
        }
        [HttpGet("{id}", Name = "GetBreedById")]
        [OutputCache(Tags = [cacheTag])]
        public async Task<ActionResult<BreedResponseDTO>> Get(int id)
        {
            return await Get<Breed, BreedResponseDTO>(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BreedRequestDTO breedRequestDTO)
        {
            return await Post<BreedRequestDTO, Breed, BreedResponseDTO>(breedRequestDTO, "GetBreedById");
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] BreedUpdateRequestDTO breedUpdateRequestDTO)
        {
            return await Put<BreedUpdateRequestDTO, Breed>(id, breedUpdateRequestDTO);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await Delete<Breed>(id);
        }
    }
}
