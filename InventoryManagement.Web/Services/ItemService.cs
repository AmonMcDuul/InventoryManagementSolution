using InventoryManagement.Models.Dtos;
using InventoryManagement.Web.Services.Contracts;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace InventoryManagement.Web.Services
{
    public class ItemService : IItemService
    {
        private readonly HttpClient httpClient;

        public ItemService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<ItemToAddDto> AddItem(ItemToAddDto itemToAddDto)
        {
            try
            {
                var item = await httpClient.PostAsJsonAsync<ItemToAddDto>("api/item", itemToAddDto);

                if (item.IsSuccessStatusCode)
                {
                    if (item.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(ItemToAddDto);
                    }
                    return await item.Content.ReadFromJsonAsync<ItemToAddDto>();
                }
                else
                {
                    var message = await item.Content.ReadAsStringAsync();
                    throw new Exception($"Http status:{item.StatusCode} Message --{message}");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ItemDto> DeleteItem(int id)
        {
            try
            {
                var item = await httpClient.DeleteAsync($"api/item/{id}");
                if (item.IsSuccessStatusCode)
                {
                    return await item.Content.ReadFromJsonAsync<ItemDto>();
                }
                return default(ItemDto);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ItemDto> GetItem(int id)
        {
            try
            {
                var item = await httpClient.GetAsync($"api/item/{id}");
                if (item.IsSuccessStatusCode)
                {
                    if (item.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(ItemDto);
                    }
                    return await item.Content.ReadFromJsonAsync<ItemDto>();
                }
                else
                {
                    var message = await item.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<ItemDto>> GetItems()
        {
            try
            {
                var items = await this.httpClient.GetAsync("api/item");

                if (items.IsSuccessStatusCode)
                {
                    if (items.StatusCode== System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<ItemDto>();
                    }
                    return await items.Content.ReadFromJsonAsync<IEnumerable<ItemDto>>();
                }
                else
                {
                    var message = await items.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ItemDto> UpdateItem(ItemToUpdateDto itemToUpdateDto)
        {
            try
            {
                var jsonRequest = JsonConvert.SerializeObject(itemToUpdateDto);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json-patch+json");

                var response = await httpClient.PatchAsync($"api/item/{itemToUpdateDto.Id}", content);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<ItemDto>();
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
