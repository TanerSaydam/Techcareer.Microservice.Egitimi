using eTicaret.AuthWebAPI.Models.Shared;
using eTicaret.AuthWebAPI.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eTicaret.AuthWebAPI.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .Property(p => p.Id)
            .HasConversion(id => id.Value, value => IdentityKey<Guid>.SetId(value));

        builder
            .Property(p => p.FirstName)
            .HasConversion(firstName => firstName.Value, value => new FirstName(value))
            .HasColumnType("varchar(100)");

        builder
            .Property(p => p.LastName)
            .HasConversion(lastName => lastName.Value, value => new LastName(value))
            .HasColumnType("varchar(100)");
    }
}