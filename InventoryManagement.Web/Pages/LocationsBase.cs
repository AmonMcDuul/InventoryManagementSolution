using InventoryManagement.Models.Dtos;
using InventoryManagement.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace InventoryManagement.Web.Pages
{
    public class LocationsBase : ComponentBase
    {
        [Inject]
        public ILocationService LocationService { get; set; }
        public IEnumerable<ItemLocationDto> Locations { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Locations = await LocationService.GetLocations();
        }

        protected async Task AddLocation_Click(ItemLocationDto itemLocationDto)
        {
            try
            {
                var locationDto = await LocationService.AddLocation(itemLocationDto);
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
        }
        protected async Task DeleteLocation_Click(int id)
        {
            var locationDto = await LocationService.DeleteLocation(id);
        }
    }
}