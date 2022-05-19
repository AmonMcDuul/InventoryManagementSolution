using InventoryManagement.Api.Extensions;
using InventoryManagement.Api.Repositories.Contracts;
using InventoryManagement.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Api.Controllers
{

    //API Controller. Handles CRUD operations for category entity. Update operation not included.

    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IItemRepository itemRepository;
        public CategoryController(IItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemCategoryDto>>> GetCategories()
        {
            try
            {
                var categories = await this.itemRepository.GetCategories();

                if (categories == null)
                {
                    return NotFound();
                }
                else
                {
                    var itemCategoriesDtos = categories.ConvertToCategoryDto();
                    return Ok(itemCategoriesDtos);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ItemCategoryDto>> GetCategory(int id)
        {
            try
            {
                var category = await this.itemRepository.GetCategory(id);

                if (category == null)
                {
                    return BadRequest();
                }
                else
                {
                    var categoryDto = category.ConvertToCategoryDto();
                    return Ok(categoryDto);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ItemCategoryDto>> PostItem([FromBody] ItemCategoryDto itemCategoryDto)
        {
            try
            {
                var newCategory = await this.itemRepository.AddCategory(itemCategoryDto);

                if (newCategory == null)
                {
                    return NoContent();
                }
                var category = await itemRepository.GetCategory(newCategory.Id);

                var newCategoryDto = newCategory.ConvertToCategoryDto(category);
                return CreatedAtAction(nameof(GetCategory), new { id = newCategoryDto.Id }, newCategoryDto);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ItemCategoryDto>> DeleteCategory(int id)
        {
            try
            {
                var items = await this.itemRepository.GetItems();
                var categoryIsUsed = items.Any(x => x.CategoryId == id);
                if (categoryIsUsed)
                {
                    return Forbid();
                }

                var category = await this.itemRepository.DeleteCategory(id);

                if (category == null)
                {
                    return NotFound();
                }

                return Ok(category);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
