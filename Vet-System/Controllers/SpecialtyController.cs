using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Vet_Application.DTOs.Response;
using Vet_Domain.Entities;
using Vet_Infrastructure.Data;
using Vet_Infrastructure.Services.Interfaces;
using Vet_System.Controllers.Base;
using Vet_System.Services.DTOs.Response;

namespace Vet_System.Controllers
{
    [ApiController]
    [Route("api/specialty")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Policy = "IsAdmin")]

    public class SpecialtyController : CustomBaseController
    {
        private readonly IOutputCacheStore outputCacheStore;
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IMapper mapper;
        private readonly IFileStorage fileStorage;
        private const string cacheTag = "specialty";
        private readonly string container = "specialty";

        public SpecialtyController(IOutputCacheStore outputCacheStore, ApplicationDbContext applicationDbContext, IMapper mapper, IFileStorage fileStorage)
            : base(applicationDbContext, mapper, outputCacheStore, cacheTag, fileStorage)
        {
            this.outputCacheStore = outputCacheStore;
            this.applicationDbContext = applicationDbContext;
            this.mapper = mapper;
            this.fileStorage = fileStorage;
        }

        [HttpGet]
        [OutputCache(Tags = [cacheTag])]
        public async Task<List<SpecialtyResponseDTO>> Get([FromQuery] PaginationResponseDTO paginationResponseDTO)
        {
            return await Get<Specialty, SpecialtyResponseDTO>(paginationResponseDTO, c => c.Id);
        }
        [HttpGet("{id}", Name = "GetSpecialtyById")]
        [OutputCache(Tags = [cacheTag])]
        public async Task<ActionResult<SpecialtyResponseDTO>> Get(int id)
        {
            return await Get<Specialty, SpecialtyResponseDTO>(id);
        }
    }
}
