using Icarus.Domain.Primitives;
using Icarus.Domain.Shared;

namespace Icarus.Domain.ValueObjects;

public sealed class Email : ValueObject
{
    public const int MaxLength = 50;
    public Email(string val)
    {
        Value = val;
    }

    public string Value { get; }

    public static Result<Email> Create(string Email)
    {
        if (string.IsNullOrEmpty(Email))
        {
            return Result.Failure<Email>(new Error(
            "Email.Empty",
            "Email is empty"
            ));
        }

        if (Email.Length > MaxLength)
        {
            return Result.Failure<Email>(new Error(
                "Email.TooLong",
                "Email is too long"
                ));
        }

        return new Email(Email);
    }
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
