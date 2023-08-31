using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.Configurations;

public class ProductConfiguration : EntityConfiguration<Product>
{
    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        base.Configure(builder);
        builder.Property(_ => _.Name).IsRequired();
        builder.Property(_ => _.Value).IsRequired();
        builder.HasOne(_ => _.User).WithMany(_ => _.Products).HasForeignKey(_ => _.UserId);
    }
}
