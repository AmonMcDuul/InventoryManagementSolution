using InventoryManagement.Models.Dtos;

namespace InventoryManagement.Web.Services.Contracts
{
    public interface IItemService
    {
        Task<IEnumerable<ItemDto>> GetItems();
        Task<ItemDto> GetItem(int id);
        Task<ItemToAddDto> AddItem(ItemToAddDto itemToAddDto);
        Task<ItemDto> DeleteItem(int id);
        Task<ItemDto> UpdateItem(ItemToUpdateDto itemToUpdateDto);
    }
}
