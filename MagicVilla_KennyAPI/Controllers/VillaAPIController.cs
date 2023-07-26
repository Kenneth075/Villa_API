using MagicVilla_KennyAPI.Data;
using MagicVilla_KennyAPI.Model;
using MagicVilla_KennyAPI.Model.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_KennyAPI.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        //Implementing logger through dependancy injection.

        private readonly ILogger<VillaAPIController> _logger;

        public VillaAPIController(ILogger<VillaAPIController>logger)
        {
            _logger = logger;
            
        }



        //Creating an endpoint
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()  //introducing ActionResult to define the statuscodes
        {
            _logger.LogInformation("Get all the villas");

            return Ok(DataStore.VillaList);

        }

        //Getting a single endpoint.

        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]        //Adding documentation.
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(200, type = typeof(VillaDTO))]    //For documentation
        public ActionResult<VillaDTO> GetVilla(int id)
        {
            //Adding multiple statuscode and validation
            if (id == 0)
            {
                _logger.LogError("Get villa error with ID" + id);

                return BadRequest();
            }
            var villa = DataStore.VillaList.FirstOrDefault(u => u.Id == id);
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
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);

            //}

            //Validation to check if villa is unique, i.e does not repeat same name.
            if (DataStore.VillaList.FirstOrDefault(u => u.Name.ToLower() == villaDto.Name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Villa already exists!");
                return BadRequest(ModelState);
            }


            if (villaDto == null)
            {
                return BadRequest(villaDto);
            }
            if (villaDto.Id > 0)      //A create request id most not be greater than zero.
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            villaDto.Id = DataStore.VillaList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;   //Increasing the id in descending order.
            DataStore.VillaList.Add(villaDto);

            //return Ok(villaDto);
            return CreatedAtRoute("GetVilla", new { id = villaDto.Id }, villaDto);   //Use CreatedAtRoute to get the HttpPost URL.

        }

        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult DeleteVilla(int id)    //Note we are using IActionResult because we are not defining the return type.
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = DataStore.VillaList.FirstOrDefault(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            DataStore.VillaList.Remove(villa);
            return NoContent();
        }
        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        public IActionResult UpdateVilla(int id, [FromBody] VillaDTO villaDTO)
        {
            if(villaDTO == null || id != villaDTO.Id)
            {
                return BadRequest();
            }
            var villa = DataStore.VillaList.FirstOrDefault(u => u.Id == id);
            villa.Name = villaDTO.Name;
            villa.Sqft = villaDTO.Sqft;
            villa.Occupancy = villaDTO.Occupancy;

            return NoContent();

        }
        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> patchDTO)
        {
            if(patchDTO == null || id == 0)
            {
                return BadRequest();
            }
            var villa = DataStore.VillaList.FirstOrDefault(u=>u.Id == id);
            if(villa == null)
            {
                return BadRequest();
            }
            patchDTO.ApplyTo(villa, ModelState);
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }


    }
}
