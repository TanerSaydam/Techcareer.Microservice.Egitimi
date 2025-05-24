using eTicaret.AuthWebAPI.Models.Roles;
using eTicaret.AuthWebAPI.Models.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eTicaret.AuthWebAPI.Configurations;

public sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder
            .Property(p => p.Id)
            .HasConversion(id => id.Value, value => IdentityKey<Guid>.SetId(value));
    }
}