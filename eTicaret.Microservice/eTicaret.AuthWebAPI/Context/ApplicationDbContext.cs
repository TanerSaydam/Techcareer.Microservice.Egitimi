using eTicaret.AuthWebAPI.Models.Roles;
using eTicaret.AuthWebAPI.Models.Shared;
using eTicaret.AuthWebAPI.Models.UserRoles;
using eTicaret.AuthWebAPI.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eTicaret.AuthWebAPI.Context;

public sealed class ApplicationDbContext : IdentityDbContext<User, Role, IdentityKey<Guid>>
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<UserRole> UserRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        builder.Ignore<IdentityUserRole<IdentityKey<Guid>>>();
        builder.Ignore<IdentityUserLogin<IdentityKey<Guid>>>();
        builder.Ignore<IdentityUserClaim<IdentityKey<Guid>>>();
        builder.Ignore<IdentityUserToken<IdentityKey<Guid>>>();
        builder.Ignore<IdentityRoleClaim<IdentityKey<Guid>>>();
    }
}
