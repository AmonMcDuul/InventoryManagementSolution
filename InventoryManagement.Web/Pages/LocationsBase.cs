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
        private bool isSortedAscending;
        private string activeSortColumn;
        protected override async Task OnInitializedAsync()
        {
            Locations = await LocationService.GetLocations();
        }

        //Search functionality
        public string Filter { get; set; }

        public bool IsVisible(ItemLocationDto item)
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
                Locations = Locations.OrderBy(x => x.GetType().GetProperty(columnName).GetValue(x, null)).ToList();
                isSortedAscending = true;
                activeSortColumn = columnName;

            }
            else
            {
                if (isSortedAscending)
                {
                    Locations = Locations.OrderByDescending(x => x.GetType().GetProperty(columnName).GetValue(x, null)).ToList();
                }
                else
                {
                    Locations = Locations.OrderBy(x => x.GetType().GetProperty(columnName).GetValue(x, null)).ToList();
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
            Locations = await LocationService.GetLocations();
        }
    }
}