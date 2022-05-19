using InventoryManagement.Api.Data;
using InventoryManagement.Api.Entities;
using InventoryManagement.Api.Repositories.Contracts;
using InventoryManagement.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Api.Repositories
{

    // Repository for all data.
    ///Backlog: Could be seperated into 3 repositories -> IItemRepository / ICategoryRepository / ILocationRepository
    public class ItemRepository : IItemRepository
    {
        private readonly InventoryManagementDbContext inventoryManagementDbContext;

        public ItemRepository(InventoryManagementDbContext inventoryManagementDbContext)
        {
            this.inventoryManagementDbContext = inventoryManagementDbContext;
        }

        public async Task<Category> AddCategory(ItemCategoryDto itemCategoryDto)
        {
            var categoryToAdd = new Category();
            categoryToAdd.Name = itemCategoryDto.Name;
            if (categoryToAdd != null)
            {
                var result = await this.inventoryManagementDbContext.Categories.AddAsync(categoryToAdd);
                await this.inventoryManagementDbContext.SaveChangesAsync();
                return result.Entity;
            }
            return null;
        }

        public async Task<Item> AddItem(ItemToAddDto itemToAddDto)
        {
            var itemToAdd = new Item();
            itemToAdd.Name = itemToAddDto.Name;
            itemToAdd.CategoryId = itemToAddDto.CategoryId;
            itemToAdd.LocationId = itemToAddDto.LocationId;
            if (itemToAdd != null)
            {
                var result = await this.inventoryManagementDbContext.Items.AddAsync(itemToAdd);
                await this.inventoryManagementDbContext.SaveChangesAsync();
                return result.Entity;
            }
            return null;
        }

        public async Task<Location> AddLocation(ItemLocationDto itemLocationDto)
        {
            var locationToAdd = new Location();
            locationToAdd.Name = itemLocationDto.Name; 
            if (locationToAdd != null)
            {
                var result = await this.inventoryManagementDbContext.Locations.AddAsync(locationToAdd);
                await this.inventoryManagementDbContext.SaveChangesAsync();
                return result.Entity;
            }
            return null;
        }

        public async Task<Category> DeleteCategory(int id)
        {
            {
                var category = await this.inventoryManagementDbContext.Categories.FindAsync(id);

                if (category != null)
                {
                    this.inventoryManagementDbContext.Categories.Remove(category);
                    await this.inventoryManagementDbContext.SaveChangesAsync();
                }

                return category;
            }
        }
        public async Task<Item> DeleteItem(int id)
        {
            var item = await this.inventoryManagementDbContext.Items.FindAsync(id);

            if (item != null)
            {
                this.inventoryManagementDbContext.Items.Remove(item);
                await this.inventoryManagementDbContext.SaveChangesAsync();
            }

            return item;
        }

        public async Task<Location> DeleteLocation(int id)
        {
            {
                var location = await this.inventoryManagementDbContext.Locations.FindAsync(id);

                if (location != null)
                {
                    this.inventoryManagementDbContext.Locations.Remove(location);
                    await this.inventoryManagementDbContext.SaveChangesAsync();
                }

                return location;
            }
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            var categories = await this.inventoryManagementDbContext.Categories.ToListAsync();

            return categories;
        }

        public async Task<Category> GetCategory(int id)
        {
            var category = await inventoryManagementDbContext.Categories.SingleOrDefaultAsync(c => c.Id == id);
            return category;
        }

        public async Task<Item> GetItem(int id)
        {
            var item = await inventoryManagementDbContext.Items.FindAsync(id);

            return item;
        }

        public async Task<IEnumerable<Item>> GetItems()
        {
            var items = await this.inventoryManagementDbContext.Items.ToListAsync();

            return items;
        }

        public async Task<Location> GetLocation(int id)
        {
            var location = await inventoryManagementDbContext.Locations.SingleOrDefaultAsync(c => c.Id == id);
            return location;
        }

        public async Task<IEnumerable<Location>> GetLocations()
        {
            var locations = await this.inventoryManagementDbContext.Locations.ToListAsync();

            return locations;
        }

        public async Task<Item> UpdateItem(int id, ItemToUpdateDto itemToUpdateDto)
        {
            var item = await this.inventoryManagementDbContext.Items.FindAsync(id);
            if (item != null)
            {
                item.LocationId = itemToUpdateDto.LocationId;
                await this.inventoryManagementDbContext.SaveChangesAsync();
                return item;
            }
            return null;
        }
    }
}
