using InventoryManagement.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Api.Data
{
    public class InventoryManagementDbContext : DbContext
    {

		//The bridge between entity classes and the database.

		public InventoryManagementDbContext(DbContextOptions<InventoryManagementDbContext> options) : base(options)
        {

        }

		//Seeds the database after creation database.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
			//Item
			modelBuilder.Entity<Item>().HasData(new Item
			{
				Id = 1,
				Name = "Muis",
				CategoryId = 1,
				LocationId = 1,

			});
			modelBuilder.Entity<Item>().HasData(new Item
			{
				Id = 2,
				Name = "Keyboard",
				CategoryId = 3,
				LocationId = 3,

			});
			modelBuilder.Entity<Item>().HasData(new Item
			{
				Id = 3,
				Name = "Muis",
				CategoryId = 2,
				LocationId = 2,

			});
			modelBuilder.Entity<Item>().HasData(new Item
			{
				Id = 4,
				Name = "Muis",
				CategoryId = 3,
				LocationId = 1,

			});
			modelBuilder.Entity<Item>().HasData(new Item
			{
				Id = 5,
				Name = "Keyboard",
				CategoryId = 1,
				LocationId = 2,

			});
			//Category
			modelBuilder.Entity<Category>().HasData(new Category
			{
				Id = 1,
				Name = "Logitech",
			});
			modelBuilder.Entity<Category>().HasData(new Category
			{
				Id = 2,
				Name = "Samsung",
			});
			modelBuilder.Entity<Category>().HasData(new Category
			{
				Id = 3,
				Name = "Onbekend",
			});
			//Location
			modelBuilder.Entity<Location>().HasData(new Location
			{
				Id = 1,
				Name = "Vang",
			});
			modelBuilder.Entity<Location>().HasData(new Location
			{
				Id = 2,
				Name = "Bergen op Zoom",
			});
			modelBuilder.Entity<Location>().HasData(new Location
			{
				Id = 3,
				Name = "Breda",
			});
		}

		public DbSet<Item> Items { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Location> Locations { get; set; } 
    }
}
