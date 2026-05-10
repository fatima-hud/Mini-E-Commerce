using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mini_E_Commerce.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mini_E_Commerce.Infrastructure.Configurations
{
    public class CartConfiguration : IEntityTypeConfiguration<CartModel>
    {
        public void Configure(EntityTypeBuilder<CartModel> builder)
        {
            builder.HasMany(e=>e.CartItems).WithOne(e=>e.Cart).HasForeignKey(e=>e.CartId).OnDelete(DeleteBehavior.Restrict);
            builder.HasQueryFilter(e => !e.IsDeleted);

        }
    }
}
