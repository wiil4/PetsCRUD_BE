using Microsoft.EntityFrameworkCore;

namespace BE_CRUDPets.Models
{
    public class ApplicationDbContext: DbContext
    {
        /*Through this class we could access Database, get data, send data and so on.*/

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Pet> Pets { get; set; }

    }
}
