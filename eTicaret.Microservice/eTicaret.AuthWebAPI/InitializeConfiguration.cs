using eTicaret.AuthWebAPI.Context;
using eTicaret.AuthWebAPI.Models.Shared;
using eTicaret.AuthWebAPI.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace eTicaret.AuthWebAPI;

public static class InitializeConfiguration
{
    public static WebApplication MigrateDatabase(this WebApplication app)
    {
        using (var scoped = app.Services.CreateScope())
        {
            var dbContext = scoped.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            dbContext.Database.Migrate();
        }

        return app;
    }

    public static WebApplication CreateFirstUser(this WebApplication app)
    {
        using (var scoped = app.Services.CreateScope())
        {
            var userManager = scoped.ServiceProvider.GetRequiredService<UserManager<User>>();

            if (userManager.Users.Any(p => p.UserName == "taner")) return app;

            IdentityKey<Guid> id = IdentityKey<Guid>.SetId(Guid.CreateVersion7());
            FirstName firstName = new("Taner");
            LastName lastName = new("Saydam");
            User user = User.Create(id, firstName, lastName, "tanersaydam@gmail.com", "taner");

            var result = userManager.CreateAsync(user, "1").GetAwaiter().GetResult();
        }

        return app;
    }
}