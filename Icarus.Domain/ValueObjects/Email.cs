using Icarus.Domain.Extensions;
using Icarus.Domain.Primitives;
using Icarus.Domain.Shared;
using System.Text.RegularExpressions;

namespace Icarus.Domain.ValueObjects;

public sealed class Email : ValueObject
{
    internal static readonly Email Empty = new Email();
    private const string EmailRegexPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

    public Email(string val)
    {
        Value = val;
    }

    private Email()
    {
        Value = String.Empty;
    }

    public string Value { get; }

    public static Result<Email> Create(string? email)
    {
        return email
            .ToResult(new Error("", "Email should be empty."))
            .OnSuccess(e => e?.Trim())
            .Ensure(e => e?.Length != 0, new Error("", "Email should be empty."))
            .Ensure(e => e?.Length < 256, new Error("", "Email is too long."))
            .Ensure(e => Regex.IsMatch(e, EmailRegexPattern, RegexOptions.IgnoreCase), new Error("", "Email is invalid."))
            .Map(e => e is null ? Empty : new Email(e));
    }

    public static implicit operator string(Email email) => email?.Value ?? string.Empty;

    public static explicit operator Email(string email)
    {
        Result<Email> emailResult = Create(email);

        Email? emailValue = emailResult.Value;

        return emailResult.IsFailure || emailValue is null ? Empty : emailValue;
    }
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
