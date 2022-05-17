using InventoryManagement.Models.Dtos;
using InventoryManagement.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace InventoryManagement.Web.Pages
{
    public class ItemEditBase : ComponentBase
    {
        [Parameter]
        public int Id { get; set; }
        [Inject]
        public IItemService ItemService { get; set; }
        [Inject]
        public ILocationService LocationService { get; set; }
        public IEnumerable<ItemLocationDto> Locations { get; set; }

        public ItemDto Item { get; set; }
        public string ErrorMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Item = await ItemService.GetItem(Id);
                Locations = await LocationService.GetLocations();
            }
            catch (Exception ex)
            {

                ErrorMessage = ex.Message;
            }
        }

        protected async Task UpdateItem_Click(int id, int locationId)
        {
            try
            {
                var updateItemDto = new ItemToUpdateDto
                {
                    Id = id,
                    LocationId = locationId
                };
                var returnedUpdateItemDto = await this.ItemService.UpdateItem(updateItemDto);
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
        }
    }
}
