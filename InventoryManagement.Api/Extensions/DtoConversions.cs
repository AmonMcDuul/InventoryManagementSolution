using InventoryManagement.Api.Entities;
using InventoryManagement.Models.Dtos;

namespace InventoryManagement.Api.Extensions
{
    public static class DtoConversions
    {

        //Returns a list of all items with their category and location
        public static IEnumerable<ItemDto> ConvertToDto(this IEnumerable<Item> items,
                                                            IEnumerable<Category> categories,
                                                            IEnumerable<Location> locations)
        {
            return (from item in items
                    join category in categories
                    on item.CategoryId equals category.Id
                    join location in locations
                    on item.LocationId equals location.Id
                    select new ItemDto
                    {
                        Id = item.Id,
                        Name = item.Name,
                        CategoryId = category.Id,
                        Category = category.Name,
                        LocationId = location.Id,
                        Location = location.Name
                    }).ToList();
        }

        //Returns an item with his category and location
        public static ItemDto ConvertToDto(this Item item,
                                            Category category,
                                            Location location)
        {
            return new ItemDto
            {
                Id = item.Id,
                Name = item.Name,
                CategoryId = category.Id,
                Category = category.Name,
                LocationId = location.Id,
                Location = location.Name
            };
        }

        //Returns only item data
        public static ItemDto ConvertToDto(this Item item, Item newItem)
        {
            return new ItemDto
            {
                Id = newItem.Id,
                Name = newItem.Name,
                CategoryId = newItem.Id,
                LocationId = newItem.Id
            };
        }

        //returns all locations data
        public static IEnumerable<ItemLocationDto> ConvertToLocationDto(this IEnumerable<Location> locations)
        {
            return (from location in locations
                    select new ItemLocationDto
                    {
                        Id = location.Id,
                        Name = location.Name
                    }).ToList();
        }

        //returns one location data
        public static ItemLocationDto ConvertToLocationDto(this Location location)
        {
            return new ItemLocationDto
            {
                Id = location.Id,
                Name = location.Name
            };
        }

        //used for POST request -> location
        public static ItemLocationDto ConvertToLocationDto(this Location location, Location newLocation)
        {
            return new ItemLocationDto
            {
                Id = newLocation.Id,
                Name = newLocation.Name
            };
        }

        //returns all categories data
        public static IEnumerable<ItemCategoryDto> ConvertToCategoryDto(this IEnumerable<Category> categories)
        {
            return (from category in categories
                    select new ItemCategoryDto
                    {
                        Id = category.Id,
                        Name = category.Name
                    }).ToList();
        }

        //returns one category data
        public static ItemCategoryDto ConvertToCategoryDto(this Category category)
        {
            return new ItemCategoryDto
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        //used for POST request -> category
        public static ItemCategoryDto ConvertToCategoryDto(this Category category, Category newCategory)
        {
            return new ItemCategoryDto
            {
                Id = newCategory.Id,
                Name = newCategory.Name
            };
        }
    }
}
