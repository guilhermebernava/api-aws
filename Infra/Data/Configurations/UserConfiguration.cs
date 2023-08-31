using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.Configurations;

public class UserConfiguration : EntityConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);
        builder.Property(_ => _.Password).IsRequired();
        builder.Property(_ => _.Email).IsRequired();
        builder.Property(_ => _.FullName);
    }
}
