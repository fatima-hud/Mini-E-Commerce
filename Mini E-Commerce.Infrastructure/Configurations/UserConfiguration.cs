using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mini_E_Commerce.Core.Models;

namespace Mini_E_Commerce.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserModel>
    {
        public void Configure(EntityTypeBuilder<UserModel> builder)
        {
            builder.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(e => e.LastName).IsRequired().HasMaxLength(50);
            builder.Property(e => e.Email).IsRequired();
            builder.HasIndex(e => e.Email).IsUnique();
           builder.ToTable(e => e.HasCheckConstraint("CK_User_Role", "[Role] = 'Admin' OR [Role] = 'Customer'"));
            builder.HasMany(e => e.Orders).WithOne(e => e.Customer).HasForeignKey(e => e.CustomerId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(e => e.Cart).WithOne(e => e.Customer).HasForeignKey<CartModel>(e => e.CustomerId).OnDelete(DeleteBehavior.Restrict);
            builder.HasQueryFilter(e => !e.IsDeleted);
        }

    }
}
