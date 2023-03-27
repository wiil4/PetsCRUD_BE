using AutoMapper;
using BE_CRUDPets.Models.DTO;

namespace BE_CRUDPets.Models.Profiles
{
    public class PetProfile : Profile
    {
        public PetProfile()
        {
            CreateMap<Pet, PetDTO>();
            CreateMap<PetDTO, Pet>();
        }
    }
}
