namespace BE_CRUDPets.Models.Repository
{
    public interface IPetRepository
    {
        Task<List<Pet>> GetPetsList();
        Task<Pet> GetPetById(int id);
        Task DeletePet(Pet pet);
        Task<Pet> AddPet(Pet pet);
        Task UpdatePet(Pet pet);
    }
}
