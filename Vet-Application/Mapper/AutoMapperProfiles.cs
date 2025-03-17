using AutoMapper;
using Microsoft.AspNetCore.Identity;
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
            ConfigureMappingClinicVeterinarian(geometryFactory);
            ConfigureMappingUsers();
            ConfigureVeterinarianSpecialty();
            ConfigureSpecialty();
            ConfigureMappingOwner();
            ConfigureMappingBreed();
            ConfigureMappingSpecies();
            ConfigureMappingPet();
        }
        #region ConfigureMappingPet
        private void ConfigureMappingPet()
        {
            CreateMap<Pet, PetResponseDTO>()
                .ForMember(dest => dest.BreedName, opt => opt.MapFrom(src => src.Breed.Name))
                .ForMember(dest => dest.SpeciesName, opt => opt.MapFrom(src => src.Breed.Species.Name))
                .ForMember(dest=>dest.OwnerName,opt=>opt.MapFrom(src=>src.Owner.Name));
            CreateMap<PetRequestDTO, Pet>();
            CreateMap<PetUpdateRequestDTO, Pet>();
        }
        #endregion

        #region ConfigureMappingSpecies
        private void ConfigureMappingSpecies()
        {
            CreateMap<Species, SpeciesResponseDTO>();
            CreateMap<SpeciesRequestDTO, Species>();
            CreateMap<SpeciesUpdateRequestDTO, Species>();
        }
        #endregion

        #region ConfigureMappingBreed
        private void ConfigureMappingBreed()
        {
            CreateMap<Breed, BreedResponseDTO>();
            CreateMap<BreedRequestDTO, Breed>();
            CreateMap<BreedUpdateRequestDTO, Breed>();
        }
        #endregion

        #region ConfigureMappingOwner
        private void ConfigureMappingOwner()
        {
            CreateMap<Owner, OwnerResponseDTO>();
            CreateMap<OwnerRequestDTO, Owner>().ForMember(dest => dest.RegistrationDate, opt => opt.MapFrom(src => DateTime.UtcNow));
            CreateMap<OwnerUpdateRequestDTO, Owner>().ForMember(dest => dest.RegistrationDate, opt => opt.Ignore());
        }
        #endregion

        #region Specialty
        private void ConfigureSpecialty()
        {
            CreateMap<Specialty, SpecialtyResponseDTO>();
            CreateMap<SpecialtyRequestDTO, Specialty>();
            CreateMap<SpecialtyUpdateRequestDTO, Specialty>();
        }
        #endregion

        #region ConfigureVeterinarianSpecialty
        private void ConfigureVeterinarianSpecialty()
        {
            CreateMap<VeterinarianSpecialty, VeterinarianSpecialtyResponseDTO>();
            CreateMap<VeterinarianSpecialtyRequestDTO, VeterinarianSpecialty>();
            CreateMap<VeterinarianSpecialtyUpdateRequestDTO, VeterinarianSpecialty>();
        }
        #endregion

        #region ConfigureMappingUsers
        private void ConfigureMappingUsers()
        {
            CreateMap<IdentityUser, UserResponseDTO>();
        }
        #endregion

        #region ConfigureMappingClinicVeterinarian
        private void ConfigureMappingClinicVeterinarian(GeometryFactory geometryFactory)
        {
            CreateMap<ClinicVeterinarian, ClinicVeterinarianResponseDTO>()
                .ForMember(dest => dest.Clinic, opt => opt.MapFrom(src => src.Clinic))
                .ForMember(desst => desst.Veterinarian, opt => opt.MapFrom(src => src.Veterinarian));
            CreateMap<ClinicVeterinarianRequestDTO, ClinicVeterinarian>();
            CreateMap<ClinicVeterinarianUpdateRequestDTO, ClinicVeterinarian>();
        }
        #endregion

        #region ConfigureMappingVeterinarian
        private void ConfigureMappingVeterinarian()
        {
            CreateMap<VeterinarianRequestDTO, Veterinarian>()
                     .ForMember(x => x.UrlImage, options => options.Ignore());
            CreateMap<VeterinarianUpdateRequestDTO, Veterinarian>()
                .ForMember(x => x.UrlImage, options => options.Ignore());
            CreateMap<Veterinarian, VeterinarianResponseDTO>();
            CreateMap<VeterinarianResponseDTO, Veterinarian>();
        }
        #endregion

        #region ConfigureMappingClinic
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
                .ForMember(dest => dest.Veterinarians, opt => opt.MapFrom(src => src.ClinicVeterinarians.Select(cv => cv.Veterinarian)));
            CreateMap<ClinicResponseDTO, Clinic>();
        }
        #endregion


    }
}
