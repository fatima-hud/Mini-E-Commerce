using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniECommerce.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniECommerce.Infrastructure.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<CategoryModel>
    {
        public void Configure(EntityTypeBuilder<CategoryModel> builder)
        {
            builder.Property(e => e.Name).IsRequired().HasMaxLength(50);
            builder.HasQueryFilter(e => !e.IsDeleted);
        }
    }
}
