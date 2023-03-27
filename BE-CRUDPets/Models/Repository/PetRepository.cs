using Microsoft.EntityFrameworkCore;

namespace BE_CRUDPets.Models.Repository
{
    public class PetRepository : IPetRepository
    {
        private readonly ApplicationDbContext _context;

        public PetRepository(ApplicationDbContext context)
        {
            _context = context;
        }        

        public async Task<List<Pet>> GetPetsList()
        {
            return await _context.Pets.ToListAsync();
        }

        public async Task<Pet> GetPetById(int id)
        {
            return await _context.Pets.FindAsync(id);
        }

        public async Task DeletePet(Pet pet)
        {
            _context.Pets.Remove(pet);
            await _context.SaveChangesAsync();
        }

        public async Task<Pet> AddPet(Pet pet)
        {
            _context.Add(pet);
            await _context.SaveChangesAsync();
            return pet;
        }

        public async Task UpdatePet(Pet pet)
        {
            var petItem = await _context.Pets.FirstOrDefaultAsync(x => x.Id == pet.Id);

            if (petItem != null)
            {
                petItem.Name = pet.Name;
                petItem.Race = pet.Race;
                petItem.Color = pet.Color;
                petItem.Age = pet.Age;
                petItem.Weight = pet.Weight;
                await _context.SaveChangesAsync();
            }            
        }
    }
}
