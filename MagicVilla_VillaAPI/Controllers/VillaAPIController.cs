﻿using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Logging;
using Microsoft.Data.Sql;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.DTOs;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using MagicVilla_VillaAPI.Repository.IRepository;

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
        // private readonly ApplicationDBContext _dbContext;
        private readonly IVillaRepository _dbVilla;
        private readonly IMapper _mapper;

        //implement Loggers as Dependency Injection 
        public VillaAPIController(IVillaRepository dbVilla, IMapper mapper)
        {
            _dbVilla = dbVilla;
            _mapper = mapper;
          
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VillaDTO>>> GetVillas()
        {
            IEnumerable<Villa> VillaList = await _dbVilla.GetAllAsync();
            IEnumerable<VillaDTO> VillaDTOList = _mapper.Map<IEnumerable<VillaDTO>>(VillaList);
            return Ok(VillaDTOList);
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
            var villa = await _dbVilla.GetAsync(x => x.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<VillaDTO>(villa));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VillaDTO>> CreateVilla([FromBody] VillaCreateDTO villaCreateDTO)
        {
            if (!ModelState.IsValid)
            {
                //if we miss ApiController, we still want to validate our villaDTo object i.e. ModelState
                return BadRequest(ModelState);
            }
            //custom validation
            if (await _dbVilla.GetAsync(u => u.Name.ToLower() == villaCreateDTO.Name.ToLower())!=null)
            {
                ModelState.AddModelError("Custom Error","This Name already Exists!");
                return BadRequest(ModelState);
            }
            if (villaCreateDTO == null)
            {
                return BadRequest();
            }
                Villa model=  _mapper.Map<Villa>(villaCreateDTO);

            await _dbVilla.CreateAsync(model);
                      
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
            var villa = await _dbVilla.GetAsync(u => u.Id == id);
            if(villa == null)
            {
                return NotFound();
            }
            await _dbVilla.DeleteAsync(villa);
            return NoContent();
        }

        [HttpPut("{id:int}",Name ="UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateVilla(int id,[FromBody] VillaUpdateDTO updateVillaDto)
        {
            if(updateVillaDto == null || id != updateVillaDto.Id)
            {
                return BadRequest();
            }

            if (updateVillaDto.Id == 0)
            {
                return NotFound(updateVillaDto);
            }

        
            Villa model = _mapper.Map<Villa>(updateVillaDto);
         
            await _dbVilla.UpdateAsync(model);
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
                    
            var villa = await _dbVilla.GetAsync(u => u.Id == id,tracked:false);
            if(villa == null)
            {
                return BadRequest();
            }

            VillaUpdateDTO villaUpdateDto = _mapper.Map<VillaUpdateDTO>(villa);

            patchDTO.ApplyTo(villaUpdateDto, ModelState);
            //convert back villaDto to villa

            Villa model = _mapper.Map<Villa>(villaUpdateDto);

            await _dbVilla.UpdateAsync(model);
           
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            return NoContent();
        }
    }
}
