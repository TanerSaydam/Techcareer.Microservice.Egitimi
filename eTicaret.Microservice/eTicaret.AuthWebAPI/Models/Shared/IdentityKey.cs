namespace eTicaret.AuthWebAPI.Models.Shared;

public record IdentityKey<T>
{
    public T Value { get; private set; }
    private IdentityKey(T value)
    {
        Value = value;
    }

    public static IdentityKey<T> SetId(T value)
    {
        //ek kontroller
        return new(value);
    }
}