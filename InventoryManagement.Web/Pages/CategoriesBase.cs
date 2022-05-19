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

        private bool isSortedAscending;
        private string activeSortColumn;

        protected override async Task OnInitializedAsync()
        {
            Category = await CategoryService.GetCategories();
        }

        //Search functionality
        public string Filter { get; set; }

        public bool IsVisible(ItemCategoryDto item)
        {
            if (string.IsNullOrEmpty(Filter))
                return true;

            if (item.Name.Contains(Filter, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        }

        //Sorting functionality
        public void SortTable(string columnName)
        {
            if (columnName != activeSortColumn)
            {
                Category = Category.OrderBy(x => x.GetType().GetProperty(columnName).GetValue(x, null)).ToList();
                isSortedAscending = true;
                activeSortColumn = columnName;

            }
            else
            {
                if (isSortedAscending)
                {
                    Category = Category.OrderByDescending(x => x.GetType().GetProperty(columnName).GetValue(x, null)).ToList();
                }
                else
                {
                    Category = Category.OrderBy(x => x.GetType().GetProperty(columnName).GetValue(x, null)).ToList();
                }

                isSortedAscending = !isSortedAscending;
            }
        }

        public string SetSortIcon(string columnName)
        {
            if (activeSortColumn != columnName)
            {
                return string.Empty;
            }
            if (isSortedAscending)
            {
                return "fa-sort-up";
            }
            else
            {
                return "fa-sort-down";
            }
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
            Category = await CategoryService.GetCategories();
        }
    }
}