using InventoryManagement.Web;
using InventoryManagement.Web.Services;
using InventoryManagement.Web.Services.Contracts;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//Dependency injections
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7088/") });
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ILocationService, LocationService>();

await builder.Build().RunAsync();
