using AutoMapper;
using Vet_Application.DTOs.Request;
using Vet_Application.DTOs.Response;
using Vet_Domain.Entities;


namespace Vet_Application.Mapper
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
                .ForMember(x => x.UrlLogo, options => options.Ignore());
            CreateMap<ClinicUpdateRequestDTO, Clinic>()
                .ForMember(x => x.UrlLogo, options => options.Ignore());
            CreateMap<Clinic, ClinicResponseDTO>();
            CreateMap<ClinicResponseDTO, Clinic>();
        }
    }
}
