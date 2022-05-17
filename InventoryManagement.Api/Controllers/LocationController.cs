using InventoryManagement.Api.Extensions;
using InventoryManagement.Api.Repositories.Contracts;
using InventoryManagement.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Api.Controllers
{

    //API Controller. Handles CRUD operations for location entity. Update operation not included.

    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly IItemRepository itemRepository;

        public LocationController(IItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemLocationDto>>> GetLocations()
        {
            try
            {
                var locations = await this.itemRepository.GetLocations();

                if (locations == null)
                {
                    return NotFound();
                }
                else
                {
                    var itemLocationDtos = locations.ConvertToLocationDto();
                    return Ok(itemLocationDtos);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ItemLocationDto>> GetLocation(int id)
        {
            try
            {
                var location = await this.itemRepository.GetLocation(id);

                if (location == null)
                {
                    return BadRequest();
                }
                else
                {
                    var locationDto = location.ConvertToLocationDto();
                    return Ok(locationDto);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ItemLocationDto>> PostItem([FromBody] ItemLocationDto itemLocationDto)
        {
            try
            {
                var newLocation = await this.itemRepository.AddLocation(itemLocationDto);

                if (newLocation == null)
                {
                    return NoContent();
                }
                var location = await itemRepository.GetLocation(newLocation.Id);

                var newLocationDto = newLocation.ConvertToLocationDto(location);
                return CreatedAtAction(nameof(GetLocation), new { id = newLocationDto.Id }, newLocationDto);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ItemLocationDto>> DeleteLocation(int id)
        {
            try
            {
                var location = await this.itemRepository.DeleteLocation(id);

                if (location == null)
                {
                    return NotFound();
                }
          
                return Ok(location);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}