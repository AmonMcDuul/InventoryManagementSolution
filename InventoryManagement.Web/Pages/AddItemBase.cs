using InventoryManagement.Models.Dtos;
using InventoryManagement.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace InventoryManagement.Web.Pages
{
    public class AddItemBase : ComponentBase
    {
        [Inject]
        public IItemService ItemService { get; set; }
        [Inject]
        public ICategoryService CategoryService { get; set; }
        [Inject]
        public ILocationService LocationService { get; set; }
        public IEnumerable<ItemDto> Items { get; set; }
        public IEnumerable<ItemCategoryDto> Category { get; set; }
        public IEnumerable<ItemLocationDto> Locations { get; set; }


        protected override async Task OnInitializedAsync()
        {
            Items = await ItemService.GetItems();
            Category = await CategoryService.GetCategories();
            Locations = await LocationService.GetLocations();
        }

        protected async Task AddItem_Click(ItemToAddDto itemToAddDto)
        {
            try
            {
                var itemDto = await ItemService.AddItem(itemToAddDto);
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
        }
    }
}
