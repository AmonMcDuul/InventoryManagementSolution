using InventoryManagement.Models.Dtos;
using InventoryManagement.Web.Services.Contracts;
using System.Net.Http.Json;

namespace InventoryManagement.Web.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient httpClient;

        public CategoryService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<ItemCategoryDto> AddCategory(ItemCategoryDto itemCategoryDto)
        {
            try
            {
                var category = await httpClient.PostAsJsonAsync<ItemCategoryDto>("api/category", itemCategoryDto);

                if (category.IsSuccessStatusCode)
                {
                    if (category.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(ItemCategoryDto);
                    }
                    return await category.Content.ReadFromJsonAsync<ItemCategoryDto>();
                }
                else
                {
                    var message = await category.Content.ReadAsStringAsync();
                    throw new Exception($"Http status:{category.StatusCode} Message --{message}");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ItemCategoryDto> DeleteCategory(int id)
        {
            try
            {
                var category = await httpClient.DeleteAsync($"api/category/{id}");
                if (category.IsSuccessStatusCode)
                {
                    return await category.Content.ReadFromJsonAsync<ItemCategoryDto>();
                }
                return default(ItemCategoryDto);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<ItemCategoryDto>> GetCategories()
        {
            try
            {
                var categories = await this.httpClient.GetAsync("api/category");

                if (categories.IsSuccessStatusCode)
                {
                    if (categories.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<ItemCategoryDto>();
                    }
                    return await categories.Content.ReadFromJsonAsync<IEnumerable<ItemCategoryDto>>();
                }
                else
                {
                    var message = await categories.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ItemCategoryDto> GetCategory(int id)
        {
            try
            {
                var category = await httpClient.GetAsync($"api/category/{id}");
                if (category.IsSuccessStatusCode)
                {
                    if (category.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(ItemCategoryDto);
                    }
                    return await category.Content.ReadFromJsonAsync<ItemCategoryDto>();
                }
                else
                {
                    var message = await category.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
