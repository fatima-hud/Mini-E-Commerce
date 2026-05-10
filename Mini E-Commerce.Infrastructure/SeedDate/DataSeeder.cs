using Microsoft.EntityFrameworkCore;
using Mini_E_Commerce.Core.Models;
using Mini_E_Commerce.Infrastructure.Abstracts;
using Mini_E_Commerce.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mini_E_Commerce.Infrastructure.SeedDate
{
    public static class DataSeeder
    {
     

        public static async Task SeedDataAsync(ApplicationDbContext dbContext)
        {
            if (!dbContext.Users.Any())
            {

                var users = new List<UserModel>
            {
                new UserModel { Id = Guid.NewGuid(), FirstName = "Ali",LastName="Ali", Email = "Ali@gmail.com",Role="Admin"},
                new UserModel { Id = Guid.NewGuid(), FirstName = "Jana",LastName="Ali", Email = "Jana@gmail.com",Role="Customer"},
                new UserModel { Id = Guid.NewGuid(), FirstName = "Alice",LastName="Johnson", Email = "Alice@gmail.com",Role="Customer"},
            };
                await dbContext.Users.AddRangeAsync(users);
                await dbContext.SaveChangesAsync();
            }
            if (!dbContext.Categories.Any())
            {
                var categories = new List<CategoryModel>
            {
                new CategoryModel { Id = Guid.NewGuid(), Name = "Electronics" },
                new CategoryModel { Id = Guid.NewGuid(), Name = "Books" },
                new CategoryModel { Id = Guid.NewGuid(), Name = "Clothing" },
            };
            await dbContext.Categories.AddRangeAsync(categories);
                await dbContext.SaveChangesAsync();
            }
            if (!dbContext.Products.Any())
            {
                var products= new List<ProductModel>
                {
                    new ProductModel { Id = Guid.NewGuid(),Name="Smartphone",Description="Latest model smartphone",StockQuantity=50,Price=500m,CategoryId=dbContext.Categories.FirstOrDefault(c=>c.Name=="Electronics").Id },
                    new ProductModel { Id = Guid.NewGuid(),Name="Laptop",Description="High-performance laptop",StockQuantity=30,Price=1000m,CategoryId=dbContext.Categories.FirstOrDefault(c=>c.Name=="Electronics").Id },
                    new ProductModel { Id = Guid.NewGuid(),Name="book1",Description="book1 description",StockQuantity=50,Price=50m,CategoryId=dbContext.Categories.FirstOrDefault(c=>c.Name=="Books").Id },
                    new ProductModel { Id = Guid.NewGuid(),Name="book2",Description="book2 description",StockQuantity=30,Price=40m,CategoryId=dbContext.Categories.FirstOrDefault(c=>c.Name=="Books").Id }
                };
                await dbContext.Products.AddRangeAsync(products);
                await dbContext.SaveChangesAsync();
            }
           



        }
    }
}
