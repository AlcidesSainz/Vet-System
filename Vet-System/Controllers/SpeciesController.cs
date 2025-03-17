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
    [Route("api/species")]
    public class SpeciesController:CustomBaseController
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IMapper mapper;
        private readonly IOutputCacheStore outputCacheStore;
        private readonly IFileStorage fileStorage;
        private const string cacheTag = "species";
        private readonly string container = "species";

        public SpeciesController(ApplicationDbContext applicationDbContext,IMapper mapper,IOutputCacheStore outputCacheStore,IFileStorage fileStorage)
            : base(applicationDbContext, mapper, outputCacheStore, cacheTag, fileStorage)
        {
            this.applicationDbContext = applicationDbContext;
            this.mapper = mapper;
            this.outputCacheStore = outputCacheStore;
            this.fileStorage = fileStorage;
        }
        [HttpGet]
        [OutputCache(Tags = [cacheTag])]
        public async Task<List<SpeciesResponseDTO>> Get([FromQuery] PaginationResponseDTO paginationResponseDTO)
        {
            return await Get<Species, SpeciesResponseDTO>(paginationResponseDTO, c => c.Id);
        }
        [HttpGet("{id}", Name = "GetSpeciesById")]
        [OutputCache(Tags = [cacheTag])]
        public async Task<ActionResult<SpeciesResponseDTO>> Get(int id)
        {
            return await Get<Species, SpeciesResponseDTO>(
                id
                );
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SpeciesRequestDTO speciesRequestDTO)
        {
            return await Post<SpeciesRequestDTO, Species, SpeciesResponseDTO>(speciesRequestDTO, "GetSpeciesById");
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] SpeciesUpdateRequestDTO speciesUpdateRequestDTO)
        {
            return await Put<SpeciesUpdateRequestDTO, Species>(id, speciesUpdateRequestDTO);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await Delete<Species>(id);
        }
    }
}
