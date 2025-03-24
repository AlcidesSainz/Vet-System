using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;
using System.Formats.Asn1;
using Vet_Application.DTOs.Request;
using Vet_Application.DTOs.Response;
using Vet_Application.Utilities;
using Vet_Domain.Entities;
using Vet_Domain.Interfaces;
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

        [HttpGet("all-owners")]
        [OutputCache(Tags = [cacheTag])]
        public async Task<List<OwnerResponseDTO>> GetAllOwners([FromQuery] PaginationResponseDTO paginationResponseDTO)
        {
            return await Get<Owner, OwnerResponseDTO>(paginationResponseDTO, c => c.Id);
        }
        [HttpGet("actived-owners")]
        [OutputCache(Tags = [cacheTag])]
        public async Task<List<OwnerResponseDTO>> GetActivedOwners([FromQuery]PaginationResponseDTO paginationResponseDTO)
        {
            var queryable = applicationDbContext.Set<Owner>().AsQueryable();
            await HttpContextExtensions.AddPaginationHeader(HttpContext, queryable);
            return await queryable
                .Where(o=>!o.IsDeleted)
                .OrderBy(o=>o.Id)
                .Paginate(paginationResponseDTO)
                .ProjectTo<OwnerResponseDTO>(mapper.ConfigurationProvider).ToListAsync();
        }
        [HttpGet("deleted-owners")]
        [OutputCache(Tags = [cacheTag])]
        public async Task<List<OwnerResponseDTO>> GetDelectedOwners([FromQuery] PaginationResponseDTO paginationResponseDTO)
        {
            var queryable = applicationDbContext.Set<Owner>().AsQueryable();
            await HttpContextExtensions.AddPaginationHeader(HttpContext, queryable);
            return await queryable
                .Where(o => o.IsDeleted)
                .OrderBy(o => o.Id)
                .Paginate(paginationResponseDTO)
                .ProjectTo<OwnerResponseDTO>(mapper.ConfigurationProvider).ToListAsync();
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

        [HttpPatch("{id}/soft-recover")] 
        public async Task<IActionResult> SoftRecover(int id)
        {
            var owner = await applicationDbContext.Owners.FirstOrDefaultAsync(o => o.Id == id);
            if (owner is null)
            {
                return NotFound();
            }
            owner.IsDeleted = false; 
            await applicationDbContext.SaveChangesAsync();
            await outputCacheStore.EvictByTagAsync(cacheTag, default);
            return NoContent(); 
        }

        [HttpDelete("{id}/soft-delete")]
        public async Task<IActionResult>SoftDelete(int id)
        {
            var owner = await applicationDbContext.Owners.FirstOrDefaultAsync(o => o.Id == id);
            if (owner is null)
            {
                return NotFound(); 
            }
            owner.IsDeleted = true; 
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
