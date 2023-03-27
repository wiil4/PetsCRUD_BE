using AutoMapper;
using BE_CRUDPets.Models;
using BE_CRUDPets.Models.DTO;
using BE_CRUDPets.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BE_CRUDPets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {        
        //AutoMapper
        private readonly IMapper _mapper;

        private readonly IPetRepository _petRepository;

        public PetController( IMapper mapper, IPetRepository petRepository)
        {
            _mapper = mapper;
            _petRepository = petRepository;
        }

        //Get all pets from database
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var petList = await _petRepository.GetPetsList();
                var petListDTO = _mapper.Map<IEnumerable<PetDTO>>(petList);
                return Ok(petListDTO);
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
                var pet = await _petRepository.GetPetById(id);
                if(pet == null)
                {
                    return NotFound();
                }
                var petDTO = _mapper.Map<PetDTO>(pet);
                return Ok(petDTO);
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
                var pet = await _petRepository.GetPetById(id);
                if (pet == null)
                {
                    return NotFound();
                }
                await _petRepository.DeletePet(pet);
                return NoContent();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PetDTO petDTO)
        {
            try
            {
                var pet = _mapper.Map<Pet>(petDTO);
                //Received as DTO and mapped to normal data in order to add CreationDate and Save it into DB
                pet.CreationDate = DateTime.Now;
                pet = await _petRepository.AddPet(pet);
                //Remaping as DTO in order to return the created object as a get resource from FE
                var petItemDTO = _mapper.Map<PetDTO>(pet);
                return CreatedAtAction("Get",new {id = petItemDTO.Id}, petItemDTO);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, PetDTO petDTO)
        {
            try
            {
                var pet = _mapper.Map<Pet>(petDTO);
                if (id != pet.Id)
                {
                    return BadRequest();
                }
                var petItem = await _petRepository.GetPetById(id);
                if (petItem == null)
                {
                    return NotFound();
                }                

                await _petRepository.UpdatePet(pet);
                return NoContent();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
