using BE_CRUDPets.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BE_CRUDPets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PetController(ApplicationDbContext context)
        {
            _context = context;
        }

        //Get all pets from database
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var petList = await _context.Pets.ToListAsync();
                return Ok(petList);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Get specific pet based on the received parameter(id)
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var pet = await _context.Pets.FindAsync(id);
                if(pet == null)
                {
                    return NotFound();
                }
                return Ok(pet);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var pet = await _context.Pets.FindAsync(id);
                if (pet == null)
                {
                    return NotFound();
                }
                _context.Pets.Remove(pet);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
