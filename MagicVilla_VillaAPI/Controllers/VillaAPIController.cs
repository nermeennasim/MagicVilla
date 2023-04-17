using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Logging;
using Microsoft.Data.Sql;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.DTOs;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaAPI.Controllers
{
    //[Route("api/[controller]"] we dont want to name after controller, if controller name change, then 
    //all the api calls or clients will get an error. so you wil have to notify them
    //instead just hard code it
    
    [Route("api/VillaAPI")]
    //[ApiController]
    public class VillaAPIController : ControllerBase
    {
       // private readonly ILogging _logger;
        private readonly ApplicationDBContext _dbContext;

        //implement Loggers as Dependency Injection 
        public VillaAPIController(ApplicationDBContext dBContext)
        {
            _dbContext = dBContext;
          
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VillaDTO>>> GetVillas()
        {
            //_logger.Log("Getting all Villas","");
            return Ok(await _dbContext.Villas.ToListAsync());
        }
        //if we want to get Villa just one based on ID
        [HttpGet("{id:int}",Name ="GetVilla")]

        // [ProducesResponseType(200,Type=typeof(VillaDTO))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<VillaDTO>> GetVilla(int id)
        {
            if (id == 0){
              //  _logger.Log("Get villa Error with Id " + id,"error");
                return BadRequest();
            }
            var villa = await _dbContext.Villas.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            return Ok(villa);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VillaDTO>> CreateVilla([FromBody] VillaCreateDTO villaDto)
        {
            if (!ModelState.IsValid)
            {
                //if we miss ApiController, we still want to validate our villaDTo object i.e. ModelState
                return BadRequest(ModelState);
            }
            //custom validation
            if (await _dbContext.Villas.FirstOrDefaultAsync(u => u.Name.ToLower() == villaDto.Name.ToLower())!=null)
            {
                ModelState.AddModelError("Custom Error","This Name already Exists!");
                return BadRequest(ModelState);
            }
            if (villaDto == null)
            {
                return BadRequest();
            }
         //   var existing = _dbContext.Villas.SingleOrDefault(c => c.Id == villaDto.Id );
           
                Villa model = new()
                {
                                        Name = villaDto.Name,
                    Amenity = villaDto.Amenity,
                    ImageUrl = villaDto.ImageUrl,
                    Rate = villaDto.Rate,
                    Sqft = villaDto.Sqft,
                    Occupancy = villaDto.Occupancy,
                    Details = villaDto.Details,
                    CreatedDate = DateTime.Now,

                };
                //we can get ids already stored in Villa Store and increment the last id
                //  villaDto.Id = _dbContext.Villas.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;

                await _dbContext.Villas.AddAsync(model);

                await _dbContext.SaveChangesAsync();

                      
            //instead of Ok result we can set a name of route where it was created
            return CreatedAtRoute("GetVilla", new {Id= model.Id}, model);
               
        }
        [HttpDelete("{id:int}",Name ="DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = await _dbContext.Villas.FirstOrDefaultAsync(u => u.Id == id);
            if(villa == null)
            {
                return NotFound();
            }

           _dbContext.Villas.Remove(villa);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id:int}",Name ="UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateVilla(int id,[FromBody] VillaUpdateDTO villaDto)
        {
            if(villaDto == null || id != villaDto.Id)
            {
                return BadRequest();
            }

            if (villaDto.Id == 0)
            {
                return NotFound(villaDto);
            }

            var villa = await _dbContext.Villas.FirstOrDefaultAsync(u => u.Id == id);
            Villa model = new ()
            {
                Id = villaDto.Id,
                Name = villaDto.Name,
                Amenity = villaDto.Amenity,
                Rate = villaDto.Rate,
                ImageUrl = villaDto.ImageUrl,
                Sqft = villaDto.Sqft,
                Occupancy = villaDto.Occupancy,
                Details = villaDto.Details,
                CreatedDate = DateTime.Now,

            };
             _dbContext.Villas.Update(model);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]


        public async Task<IActionResult> UpdatePartialVilla(int id, [FromBody] JsonPatchDocument<VillaUpdateDTO> patchDTO)
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }
                    
            var villa = await _dbContext.Villas.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
            if(villa == null)
            {
                return BadRequest();
            }

            VillaUpdateDTO villaDto = new ()
            {
                Id = villa.Id,
                Name = villa.Name,
                Amenity = villa.Amenity,
                ImageUrl = villa.ImageUrl,
                Rate = villa.Rate,
                Sqft = villa.Sqft,
                Occupancy = villa.Occupancy,
                Details = villa.Details,

            };
            patchDTO.ApplyTo(villaDto, ModelState);
            //convert back villaDto to villa

            Villa model = new()
            {
                Id= villaDto.Id,
                Name = villaDto.Name,
                Amenity = villaDto.Amenity,
                Rate = villaDto.Rate,
                ImageUrl = villaDto.ImageUrl,
                Sqft = villaDto.Sqft,
                Occupancy = villaDto.Occupancy,
                Details = villaDto.Details,

            };
           

            _dbContext.Villas.Update(model);
            await _dbContext.SaveChangesAsync();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
           
          
            return NoContent();
        }
    }
}
