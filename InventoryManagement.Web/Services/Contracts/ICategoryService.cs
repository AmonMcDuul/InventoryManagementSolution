using InventoryManagement.Models.Dtos;

namespace InventoryManagement.Web.Services.Contracts
{
    public interface ICategoryService
    {
        Task<IEnumerable<ItemCategoryDto>> GetCategories();
        Task<ItemCategoryDto> GetCategory(int id);
        Task<ItemCategoryDto> AddCategory(ItemCategoryDto itemCategoryDto);
        Task<ItemCategoryDto> DeleteCategory(int id);
    }
}
