using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mini_E_Commerce.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mini_E_Commerce.Infrastructure.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItemModel>
    {
        public void Configure(EntityTypeBuilder<OrderItemModel> builder)
        {
            builder.Property(e => e.UnitPrice).HasColumnType("decimal(8,2)");
            builder.ToTable(e => e.HasCheckConstraint("Ck_OrderItem_Quantity", "[Quantity] >0"));
        
        }
    }
}
