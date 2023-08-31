using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.Configurations;

public class OrderItemConfiguration : EntityConfiguration<OrderItem>
{
    public override void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        base.Configure(builder);
        builder.Property(_ => _.Quantity).IsRequired();
        builder.HasOne(_ => _.Product).WithOne(_ => _.OrderItem).HasForeignKey<OrderItem>(_ => _.ProductId);
        builder.HasOne(_ => _.Order).WithMany(_ => _.Items).HasForeignKey(_ => _.OrderId);
    }
}
