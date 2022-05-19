using InventoryManagement.Models.Dtos;
using InventoryManagement.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace InventoryManagement.Web.Pages
{
    public class ItemsBase : ComponentBase
    {
        [Inject]
        public IItemService ItemService { get; set; }
        public IEnumerable<ItemDto> Items { get; set; }
        public IEnumerable<ItemCategoryDto> Category { get; set; }
        public IEnumerable<ItemLocationDto> Locations { get; set; }

        private bool isSortedAscending;
        private string activeSortColumn;

        protected override async Task OnInitializedAsync()
        {
            Items = await ItemService.GetItems();
        }

        //Filtering
        public string Filter { get; set; }

        public bool IsVisible(ItemDto item)
        {
            if (string.IsNullOrEmpty(Filter))
                return true;

            if (item.Name.Contains(Filter, StringComparison.OrdinalIgnoreCase))
                return true;
            if (item.Category.Contains(Filter, StringComparison.OrdinalIgnoreCase))
                return true;
            if (item.Location.Contains(Filter, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        }

        //sort options
        public void SortTable(string columnName)
        {
            if (columnName != activeSortColumn)
            {
                Items = Items.OrderBy(x => x.GetType().GetProperty(columnName).GetValue(x, null)).ToList();
                isSortedAscending = true;
                activeSortColumn = columnName;

            }
            else
            {
                if (isSortedAscending)
                {
                    Items = Items.OrderByDescending(x => x.GetType().GetProperty(columnName).GetValue(x, null)).ToList();
                }
                else
                {
                    Items = Items.OrderBy(x => x.GetType().GetProperty(columnName).GetValue(x, null)).ToList();
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

 
        protected async Task DeleteItem_Click(int id)
        {
            var itemDto = await ItemService.DeleteItem(id);
            Items = await ItemService.GetItems();
        }
    }
}
