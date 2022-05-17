using InventoryManagement.Models.Dtos;
using InventoryManagement.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace InventoryManagement.Web.Pages
{
    public class CategoriesBase : ComponentBase
    {
        [Inject]
        public ICategoryService CategoryService { get; set; }
        public IEnumerable<ItemCategoryDto> Category { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Category = await CategoryService.GetCategories();
        }

        protected async Task AddCategory_Click(ItemCategoryDto itemCategoryDto)
        {
            try
            {
                var categoryDto = await CategoryService.AddCategory(itemCategoryDto);
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
        }
        protected async Task DeleteCategory_Click(int id)
        {
            var categoryDto = await CategoryService.DeleteCategory(id);
        }
    }
}