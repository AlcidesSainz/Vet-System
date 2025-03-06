using AutoMapper;
using NetTopologySuite.Geometries;
using Vet_Application.DTOs.Request;
using Vet_Application.DTOs.Response;
using Vet_Domain.Entities;


namespace Vet_Application.Mapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles(GeometryFactory geometryFactory)
        {
            ConfigureMappingClinic(geometryFactory);
            ConfigureMappingVeterinarian();
        }

        private void ConfigureMappingVeterinarian()
        {
            CreateMap<VeterinarianRequestDTO, Veterinarian>()
                     .ForMember(x => x.UrlImage, options => options.Ignore());
            CreateMap<VeterinarianUpdateRequestDTO, Veterinarian>()
                .ForMember(x => x.UrlImage, options => options.Ignore());
            CreateMap<Veterinarian, VeterinarianResponseDTO>();
            CreateMap<VeterinarianResponseDTO, Veterinarian>();
        }

        private void ConfigureMappingClinic(GeometryFactory geometryFactory)
        {
            CreateMap<ClinicRequestDTO, Clinic>()
                .ForMember(x => x.UrlImage, options => options.Ignore())
                .ForMember(x => x.Location, clinicDto => clinicDto.MapFrom(p => geometryFactory.CreatePoint(new Coordinate(p.Lng, p.Lat))));
            CreateMap<ClinicUpdateRequestDTO, Clinic>()
                .ForMember(x => x.UrlImage, options => options.Ignore());
            CreateMap<Clinic, ClinicResponseDTO>()
                .ForMember(x => x.Lat, clinic => clinic.MapFrom(p => p.Location.Y))
                .ForMember(x => x.Lng, clinic => clinic.MapFrom(p => p.Location.X))
                .ForMember(dest=>dest.Veterinarians,opt=>opt.MapFrom(src=>src.ClinicVeterinarians.Select(cv=>cv.Veterinarian)));
            CreateMap<ClinicResponseDTO, Clinic>();
        }
    }
}
