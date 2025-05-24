using eTicaret.AuthWebAPI.Models.Shared;
using Microsoft.AspNetCore.Identity;

namespace eTicaret.AuthWebAPI.Models.Users;

public sealed class User : IdentityUser<IdentityKey<Guid>>
{
    private User(
        IdentityKey<Guid> id,
        FirstName firstName,
        LastName lastName,
        string email,
        string userName
        )
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        UserName = userName;
    }
    public FirstName FirstName { get; private set; } = default!;
    public LastName LastName { get; private set; } = default!;
    public string FullName => $"{FirstName} {LastName}";

    public static User Create(
        IdentityKey<Guid> id,
        FirstName firstName,
        LastName lastName,
        string email,
        string userName
        )
    {
        var user = new User(id, firstName, lastName, email, userName);
        return user;
    }

    public void SetFirstName(string value)
    {
        //kurallar
        FirstName = new(value);
    }

    public void SetLastName(string value)
    {
        //kurallar
        LastName = new(value);
    }
}