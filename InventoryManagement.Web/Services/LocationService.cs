using InventoryManagement.Models.Dtos;
using InventoryManagement.Web.Services.Contracts;
using System.Net.Http.Json;

namespace InventoryManagement.Web.Services
{
    public class LocationService : ILocationService
    {
        private readonly HttpClient httpClient;

        public LocationService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<ItemLocationDto> AddLocation(ItemLocationDto itemLocationDto)
        {
            try
            {
                var location = await httpClient.PostAsJsonAsync<ItemLocationDto>("api/location", itemLocationDto);

                if (location.IsSuccessStatusCode)
                {
                    if (location.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(ItemLocationDto);
                    }
                    return await location.Content.ReadFromJsonAsync<ItemLocationDto>();
                }
                else
                {
                    var message = await location.Content.ReadAsStringAsync();
                    throw new Exception($"Http status:{location.StatusCode} Message --{message}");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ItemLocationDto> DeleteLocation(int id)
        {
            try
            {
                var location = await httpClient.DeleteAsync($"api/location/{id}");
                if (location.IsSuccessStatusCode)
                {
                    return await location.Content.ReadFromJsonAsync<ItemLocationDto>();
                }
                return default(ItemLocationDto);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ItemLocationDto> GetLocation(int id)
        {
            try
            {
                var location = await httpClient.GetAsync($"api/location/{id}");
                if (location.IsSuccessStatusCode)
                {
                    if (location.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(ItemLocationDto);
                    }
                    return await location.Content.ReadFromJsonAsync<ItemLocationDto>();
                }
                else
                {
                    var message = await location.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<ItemLocationDto>> GetLocations()
        {
            try
            {
                var locations = await this.httpClient.GetAsync("api/location");

                if (locations.IsSuccessStatusCode)
                {
                    if (locations.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<ItemLocationDto>();
                    }
                    return await locations.Content.ReadFromJsonAsync<IEnumerable<ItemLocationDto>>();
                }
                else
                {
                    var message = await locations.Content.ReadAsStringAsync();
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
