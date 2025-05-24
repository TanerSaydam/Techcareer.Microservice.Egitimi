using eTicaret.AuthWebAPI.Models.Shared;

namespace eTicaret.AuthWebAPI.Models.Abstractions;

public abstract class Entity<TKey>
{
    public IdentityKey<TKey> Id { get; set; } = default!;
}