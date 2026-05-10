using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mini_E_Commerce.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mini_E_Commerce.Infrastructure.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<ProductModel>
    {
        public void Configure(EntityTypeBuilder<ProductModel> builder)
        {
            builder.Property(e => e.Name).IsRequired().HasMaxLength(50);
            builder.Property(e => e.Description).HasMaxLength(100);
            builder.Property(e=>e.Price).IsRequired().HasColumnType("decimal(8,2)");
            builder.Property(e => e.StockQuantity).IsRequired();
           builder.ToTable(e=>e.HasCheckConstraint("CK_Product_StockQuantity", "[StockQuantity] >= 0"));
            builder.ToTable(e => e.HasCheckConstraint("CK_Product_Price", "[Price] > 0"));

            builder.HasOne(e => e.Category).WithMany(c => c.Products).HasForeignKey(e => e.CategoryId) .OnDelete(DeleteBehavior.Restrict);     
            builder.HasMany(e=>e.OrderItems).WithOne(e=>e.Product).HasForeignKey(e => e.ProductId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(e=>e.CartItems).WithOne(e=>e.Product).HasForeignKey(e => e.ProductId).OnDelete(DeleteBehavior.Restrict);
            builder.HasQueryFilter(e => !e.IsDeleted);

        }
    }
}
