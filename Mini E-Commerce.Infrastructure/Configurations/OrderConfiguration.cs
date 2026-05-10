using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mini_E_Commerce.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mini_E_Commerce.Infrastructure.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<OrderModel>
    {
        public void Configure(EntityTypeBuilder<OrderModel> builder)
        {
            builder.Property(e => e.TotalAmount).IsRequired().HasColumnType("decimal(8,2)");
            builder.Property(e => e.Status).HasConversion<string>();
            builder.HasMany(e => e.OrderItems).WithOne(e => e.Order).HasForeignKey(e => e.OrderId).OnDelete(DeleteBehavior.Restrict);
            builder.HasQueryFilter(e => !e.IsDeleted);
        }
    }
}
