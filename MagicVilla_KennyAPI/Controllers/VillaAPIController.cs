using MagicVilla_KennyAPI.Data;
using MagicVilla_KennyAPI.Model;
using MagicVilla_KennyAPI.Model.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_KennyAPI.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        //Implementing logger through dependancy injection.

        //private readonly ILogger<VillaAPIController> _logger;

        //public VillaAPIController(ILogger<VillaAPIController>logger)
        //{
        //    _logger = logger;
            
        //}

        private readonly ApplicationDbContext _db; 
        public VillaAPIController(ApplicationDbContext db)
        {
            _db = db;
            
        }



        //Creating an endpoint
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()  //introducing ActionResult to define the statuscodes
        {
            //_logger.LogInformation("Get all the villas");

            //return Ok(DataStore.VillaList);
            return Ok(_db.Villas);

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
                //_logger.LogError("Get villa error with ID" + id);

                return BadRequest();
            }
            //var villa = DataStore.VillaList.FirstOrDefault(u => u.Id == id);
            var villa = _db.Villas.FirstOrDefault(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            return Ok(villa);




        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VillaDTO> CreateVilla([FromBody] VillaDTO villaDto)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);

            //}

            //Validation to check if villa is unique, i.e does not repeat same name.
            if (_db.Villas.FirstOrDefault(u => u.Name.ToLower() == villaDto.Name.ToLower()) != null)
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

            //Automapper
            Villa model = new Villa()
            {
                Id = villaDto.Id,
                Name = villaDto.Name,
                Details = villaDto.Details,
                Sqft = villaDto.Sqft,
                Occupancy = villaDto.Occupancy,
                Amenity = villaDto.Amenity,
                imageUrl = villaDto.imageUrl
            };
            _db.Villas.Add(model);
            _db.SaveChanges();


            
            

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
            var villa = _db.Villas.FirstOrDefault(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            _db.Villas.Remove(villa);
            _db.SaveChanges();
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
            //var villa = DataStore.VillaList.FirstOrDefault(u => u.Id == id);
            //villa.Name = villaDTO.Name;
            //villa.Sqft = villaDTO.Sqft;
            //villa.Occupancy = villaDTO.Occupancy;

            Villa model = new Villa()
            {
                Id = villaDTO.Id,
                Name = villaDTO.Name,
                Details = villaDTO.Details,
                Sqft = villaDTO.Sqft,
                Occupancy = villaDTO.Occupancy,
                Amenity = villaDTO.Amenity,
                imageUrl = villaDTO.imageUrl
            };
            _db.Villas.Update(model);
            _db.SaveChanges();



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
            var villa = _db.Villas.AsNoTracking().FirstOrDefault(u=>u.Id == id);

            VillaDTO villaDTO = new VillaDTO()
            {
                Id = villa.Id,
                Name = villa.Name,
                Details = villa.Details,
                Sqft = villa.Sqft,
                Amenity = villa.Amenity,
                imageUrl = villa.imageUrl,
                Occupancy = villa.Occupancy,
            };


            if(villa == null)
            {
                return BadRequest();
            }

            patchDTO.ApplyTo(villaDTO, ModelState);

            Villa model = new Villa()
            {
                imageUrl = villa.imageUrl,
                Id = villa.Id,
                Sqft = villa.Sqft,
                Occupancy = villa.Occupancy,
                Name = villa.Name,
                Amenity = villa.Amenity,
                Details = villa.Details,

            };
            _db.Villas.Update(model);
            _db.SaveChanges();

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }


    }
}
