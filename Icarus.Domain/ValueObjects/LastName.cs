using Icarus.Domain.Primitives;
using Icarus.Domain.Shared;

namespace Icarus.Domain.ValueObjects;

public sealed class LastName : ValueObject
{
    public const int MaxLength = 50;
    public LastName(string val)
    {
        Value = val;
    }

    public string Value { get; }

    public static Result<LastName> Create(string LastName)
    {
        if (string.IsNullOrEmpty(LastName))
        {
            return Result.Failure<LastName>(new Error(
            "LastName.Empty",
            "LastName is empty"
            ));
        }

        if (LastName.Length > MaxLength)
        {
            return Result.Failure<LastName>(new Error(
                "LastName.TooLong",
                "LastName is too long"
                ));
        }

        return new LastName(LastName);
    }

    internal static LastName Empty => new LastName(string.Empty);
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
