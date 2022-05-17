using InventoryManagement.Api.Extensions;
using InventoryManagement.Api.Repositories.Contracts;
using InventoryManagement.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Api.Controllers
{

    //API Controller. Handles CRUD operations for item entity

    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemRepository itemRepository;

        public ItemController(IItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetItems()
        {
            try
            {
                var items = await this.itemRepository.GetItems();
                var categories = await this.itemRepository.GetCategories();
                var locations = await this.itemRepository.GetLocations();

                if (items == null || categories == null || locations == null)
                {
                    return NotFound();
                }
                else
                {
                    var itemDtos = items.ConvertToDto(categories, locations);
                    return Ok(itemDtos);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ItemDto>> GetItem(int id)
        {
            try
            {
                var item = await this.itemRepository.GetItem(id);

                if (item == null)
                {
                    return BadRequest();
                }
                else
                {
                    var category = await this.itemRepository.GetCategory(item.CategoryId);
                    var location = await this.itemRepository.GetLocation(item.LocationId);

                    var itemDto = item.ConvertToDto(category, location);
                    return Ok(itemDto);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ItemDto>> PostItem([FromBody] ItemToAddDto itemToAddDto)
        {
            try
            {
                var newItem = await this.itemRepository.AddItem(itemToAddDto);

                if (newItem == null)
                {
                    return NoContent();
                }
                var item = await itemRepository.GetItem(newItem.Id);

                var newItemDto = newItem.ConvertToDto(item);
                return CreatedAtAction(nameof(GetItem), new { id = newItemDto.Id }, newItemDto);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ItemDto>> DeleteItem(int id)
        {
            try
            {
                var item = await this.itemRepository.DeleteItem(id);

                if (item == null)
                {
                    return NotFound();
                }

                return Ok(item);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPatch("{id:int}")]
        public async Task<ActionResult<ItemDto>> UpdateItem(int id, ItemToUpdateDto itemToUpdateDto)
        {
            try
            {
                var item = await this.itemRepository.UpdateItem(id, itemToUpdateDto);
                if (item == null)
                {
                    return NotFound();
                }
                var product = await itemRepository.GetItem(item.Id);
                var itemDto = item.ConvertToDto(product);
                return Ok(itemDto);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
