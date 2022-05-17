using InventoryManagement.Api.Entities;
using InventoryManagement.Models.Dtos;

namespace InventoryManagement.Api.Repositories.Contracts
{
    //Repository interface for all data.
    ///Backlog: Could be seperated into 3 interfaces -> IItemRepository / ICategoryRepository / ILocationRepository
    public interface IItemRepository
    {
        Task<IEnumerable<Item>> GetItems();
        Task<IEnumerable<Category>> GetCategories();
        Task<IEnumerable<Location>> GetLocations();
        Task<Item> GetItem(int id);
        Task<Category> GetCategory(int id);
        Task<Location> GetLocation(int id);
        Task<Item> AddItem(ItemToAddDto itemToAddDto);
        Task<Item> DeleteItem(int id);
        Task<Item> UpdateItem(int id, ItemToUpdateDto itemToUpdateDto);
        Task<Category> AddCategory(ItemCategoryDto itemCategoryDto);
        Task<Category> DeleteCategory(int id);
        Task<Location> AddLocation(ItemLocationDto itemLocationDto);
        Task<Location> DeleteLocation(int id);

    }
}
