using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaAPI.Controllers
{
    //[Route("api/[controller]"] we dont want to name after controller, if controller name change, then 
    //all the api calls or clients will get an error. so you wil have to notify them
    //instead just hard code it
    //instead just hard code it
    [Route("api/VillaAPI")]
    //[ApiController]
    public class VillaAPIController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            return Ok(VillaStore.VillaList);
        }
        //if we want to get Villa just one based on ID
        [HttpGet("{id:int}",Name ="GetVilla")]

        // [ProducesResponseType(200,Type=typeof(VillaDTO))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<VillaDTO> GetVilla(int id)
        {
            if (id == 0){
                return BadRequest();
            }
            var villa = VillaStore.VillaList.FirstOrDefault(x => x.Id == id);
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
        public ActionResult<VillaDTO> CreateVilla([FromBody] VillaDTO villaDto)
        {
            if (!ModelState.IsValid)
            {
                //if we miss ApiController, we still want to validate our villaDTo object i.e. ModelState
                return BadRequest(ModelState);
            }
            //custom validation
            if (VillaStore.VillaList.FirstOrDefault(u => u?.Name?.ToLower() == villaDto?.Name?.ToLower())!=null)
            {
                ModelState.AddModelError("Custom Error","This Name already Exists!");
                return BadRequest(ModelState);
            }
            if (villaDto == null)
            {
                return BadRequest();
            }
            if (villaDto.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);

            }
            //we can get ids already stored in Villa Store and increment the last id
            villaDto.Id = VillaStore.VillaList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
            VillaStore.VillaList.Add(villaDto) ;
            Console.WriteLine(VillaStore.VillaList.Last());
            //instead of Ok result we can set a name of route where it was created
            return CreatedAtRoute("GetVilla", new {Id= villaDto.Id}, villaDto);
               
        }
        [HttpDelete("{id:int}",Name ="DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult  DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = VillaStore.VillaList.FirstOrDefault(u => u.Id == id);
            if(villa == null)
            {
                return NotFound();
            }

            VillaStore.VillaList.Remove(villa);
            return NoContent();
        }
    }
}
