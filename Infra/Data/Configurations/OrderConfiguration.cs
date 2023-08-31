using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.Configurations;

public class OrderConfiguration : EntityConfiguration<Order>
{
    public override void Configure(EntityTypeBuilder<Order> builder)
    {
        base.Configure(builder);
        builder.HasOne(_=> _.User).WithMany(_ => _.Orders).HasForeignKey(_ => _.UserId);
    }
}
