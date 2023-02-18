using Icarus.Domain.Primitives;
using Icarus.Domain.Shared;

namespace Icarus.Domain.ValueObjects;

public sealed class FirstName : ValueObject
{
    public const int MaxLength = 50;
    public FirstName(string val)
    {
        Value = val;
    }

    public string Value { get; }

    public static Result<FirstName> Create(string firstName)
    {
        if (string.IsNullOrEmpty(firstName))
        {
            return Result.Failure<FirstName>(new Error(
            "FirstName.Empty",
            "FirstName is empty"
            ));
        }

        if (firstName.Length > MaxLength)
        {
            return Result.Failure<FirstName>(new Error(
                "FirstName.TooLong",
                "FirstName is too long"
                ));
        }

        return new FirstName(firstName);
    }

    internal static FirstName Empty => new FirstName(string.Empty);
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
