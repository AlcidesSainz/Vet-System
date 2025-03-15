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
    [Route("api/owner")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Policy = "IsAdmin")]

    public class OwnerController : CustomBaseController
    {
        private readonly IOutputCacheStore outputCacheStore;
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IMapper mapper;
        private readonly IFileStorage fileStorage;
        private const string cacheTag = "owner";
        private readonly string container = "owner";

        public OwnerController(IOutputCacheStore outputCacheStore, ApplicationDbContext applicationDbContext, IMapper mapper, IFileStorage fileStorage)
        :base(applicationDbContext, mapper, outputCacheStore, cacheTag , fileStorage)
        {
            this.outputCacheStore = outputCacheStore;
            this.applicationDbContext = applicationDbContext;
            this.mapper = mapper;
            this.fileStorage = fileStorage;
        }

        [HttpGet]
        [OutputCache(Tags = [cacheTag])]
        public async Task<List<OwnerResponseDTO>> Get([FromQuery] PaginationResponseDTO paginationResponseDTO)
        {
            return await Get<Owner, OwnerResponseDTO>(paginationResponseDTO, c => c.Id);
        }
        [HttpGet("{id}", Name = "GetOwnerById")]
        [OutputCache(Tags = [cacheTag])]
        public async Task<ActionResult<OwnerResponseDTO>> Get(int id)
        {
            return await Get<Owner, OwnerResponseDTO>(id);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OwnerRequestDTO ownerRequestDTO)
        {
            return await Post<OwnerRequestDTO, Owner, OwnerResponseDTO>(ownerRequestDTO, "GetOwnerById");
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] OwnerUpdateRequestDTO ownerUpdateRequestDTO)
        {
            var existingOwner = await applicationDbContext.Owners.FindAsync(id);
            if (existingOwner is null) return NotFound();

            mapper.Map(ownerUpdateRequestDTO, existingOwner);

            await applicationDbContext.SaveChangesAsync();
            await outputCacheStore.EvictByTagAsync(cacheTag, default);
            return NoContent();
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await Delete<Owner>(id);
        }

    }
}
