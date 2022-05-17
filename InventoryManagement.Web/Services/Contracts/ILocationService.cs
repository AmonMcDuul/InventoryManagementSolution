using InventoryManagement.Models.Dtos;

namespace InventoryManagement.Web.Services.Contracts
{
    public interface ILocationService
    {
        Task<IEnumerable<ItemLocationDto>> GetLocations();
        Task<ItemLocationDto> GetLocation(int id);
        Task<ItemLocationDto> AddLocation(ItemLocationDto itemLocationDto);
        Task<ItemLocationDto> DeleteLocation(int id);
    }
}
