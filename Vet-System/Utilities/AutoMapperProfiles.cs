using AutoMapper;
using Vet_System.Model.Entities;
using Vet_System.Services.DTOs.Request;
using Vet_System.Services.DTOs.Response;

namespace Vet_System.Utilities
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            ConfigureMappingClinic();
        }

        private void ConfigureMappingClinic()
        {
            CreateMap<ClinicRequestDTO, Clinic>()
                .ForMember(x => x.Logo, options => options.Ignore());
            CreateMap<ClinicUpdateRequestDTO, Clinic>()
                .ForMember(x => x.Logo, options => options.Ignore());
            CreateMap<Clinic, ClinicResponseDTO>();
        }
    }
}
