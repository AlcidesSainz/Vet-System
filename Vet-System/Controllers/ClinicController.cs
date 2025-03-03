using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Vet_System.Controllers.Base;
using Vet_System.Model;
using Vet_System.Model.Entities;
using Vet_System.Services.DTOs.Request;
using Vet_System.Services.DTOs.Response;
using Vet_System.Services.Interfaces;

namespace Vet_System.Controllers
{
    [ApiController]
    [Route("api/clinic")]
    [OutputCache]
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
            return await Get<Clinic, ClinicResponseDTO>(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] ClinicRequestDTO clinicRequestDTO)
        {
            return await PostWithImage<ClinicRequestDTO, Clinic, ClinicResponseDTO>(clinicRequestDTO, dto => dto.Logo, container, "GetClinicById");
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put([FromForm] ClinicUpdateRequestDTO clinicUpdateRequestDTO)
        {
            return await PutWithImage<ClinicUpdateRequestDTO, Clinic>(clinicUpdateRequestDTO.Id, clinicUpdateRequestDTO, dto => dto.Logo, container);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await Delete<Clinic>(id);
        }
    }
}
