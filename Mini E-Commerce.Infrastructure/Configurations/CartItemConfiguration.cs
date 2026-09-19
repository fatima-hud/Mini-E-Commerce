using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniECommerce.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniECommerce.Infrastructure.Configurations
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItemModel>
    {
        public void Configure(EntityTypeBuilder<CartItemModel> builder)
        {
          
            builder.ToTable(e => e.HasCheckConstraint("Ck_CartItem_Quantity", "[Quantity] >0"));
            
        }
    }
}
