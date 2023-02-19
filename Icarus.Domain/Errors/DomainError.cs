using Icarus.Domain.Shared;

namespace Icarus.Domain.Errors;

public static class DomainError
{
    public static class ErrorAuthentication
    {
        public static Error InvalidEmailOrPassword => new Error(
            "Authentication.InvalidEmailOrPassword",
            "The provided email or password combination is invalid.");

        public static Error PasswordsDoNotMatch => new Error(
            "Authentication.PasswordsDoNotMatch",
            "The password and confirmation password do not match.");

        public static Error DuplicateEmail => new Error(
            "Authentication.DuplicateEmail",
            "The email is already taken.");
        public static Error UnAuthorized => new Error(
        "Authentication.UnAuthorized",
        "Authentication is UnAuthorized.");
    }

    /// <summary>
    /// Contains the first name errors.
    /// </summary>
    public static class ErrorFirstName
    {
        public static Error NullOrEmpty => new Error("FirstName.NullOrEmpty", "The first name is required.");

        public static Error LongerThanAllowed => new Error(
            "FirstName.LongerThanAllowed",
            "The first name is longer than allowed.");
    }

    /// <summary>
    /// Contains the last name errors.
    /// </summary>
    public static class ErrorLastName
    {
        public static Error NullOrEmpty => new Error("LastName.NullOrEmpty", "The last name is required.");

        public static Error LongerThanAllowed => new Error(
            "LastName.LongerThanAllowed",
            "The last name is longer than allowed.");
    }

    /// <summary>
    /// Contains the email errors.
    /// </summary>
    public static class ErrorEmail
    {
        public static Error NullOrEmpty => new Error("Email.NullOrEmpty", "The email is required.");

        public static Error LongerThanAllowed => new Error("Email.LongerThanAllowed", "The email is longer than allowed.");

        public static Error InvalidFormat => new Error("Email.InvalidFormat", "The email format is invalid.");
    }

    /// <summary>
    /// Contains the password errors.
    /// </summary>
    public static class ErrorPassword
    {
        public static Error NullOrEmpty => new Error("Password.NullOrEmpty", "The password is required.");

        public static Error TooShort => new Error("Password.TooShort", "The password is too short.");

        public static Error MissingUppercaseLetter => new Error(
            "Password.MissingUppercaseLetter",
            "The password requires at least one uppercase letter.");

        public static Error MissingLowercaseLetter => new Error(
            "Password.MissingLowercaseLetter",
            "The password requires at least one lowercase letter.");

        public static Error MissingDigit => new Error(
            "Password.MissingDigit",
            "The password requires at least one digit.");

        public static Error MissingNonAlphaNumeric => new Error(
            "Password.MissingNonAlphaNumeric",
            "The password requires at least one non-alphanumeric.");
    }

    /// <summary>
    /// Contains the expense errors.
    /// </summary>
    public static class ErrorMember
    {
        public static Error MemberIdIsRequired => new Error("Member.MemberIdMissing", "The Member identifier is required.");

        public static Error CurrencyIdIsRequired => new Error(
            "Member.Missing",
            "The currency identifier is required.");

        public static Error OccurredOnIsRequired => new Error("Member.DateMissing", "The occurred on date is required.");
    }

    /// <summary>
    /// Contains the income errors.
    /// </summary>
    public static class ErrorIncome
    {
        public static Error UserIdIsRequired => new Error("Income.UserIdMissing", "The user identifier is required.");

        public static Error CurrencyIdIsRequired => new Error(
            "Income.CurrencyMissing",
            "The currency identifier is required.");

        public static Error OccurredOnIsRequired => new Error("Income.DateMissing", "The occurred on date is required.");
    }

    /// <summary>
    /// Contains the currency errors.
    /// </summary>
    public static class ErrorCurrency
    {
        public static Error NotFound => new Error(
            "Currency.NotFound",
            "The currency with the specified identifier was not found.");
    }

    /// <summary>
    /// Contains the role errors.
    /// </summary>
    public static class ErrorRole
    {
        public static Error AtLeastOnePermissionIsRequired => new Error(
            "Role.AtLeastOnePermissionIsRequired",
            "The role must have at least one permission associated with it.");
    }

    /// <summary>
    /// Contains the general domain errors.
    /// </summary>
    public static class ErrorGeneral
    {
        public static Error BadRequest => new Error("General.BadRequest", "The server could not process the request.");

        public static Error EntityNotFound => new Error(
            "General.EntityNotFound",
            "The entity with the specified identifier was not found.");

        public static Error ServerError => new Error(
            "General.ServerError",
            "The server encountered an unrecoverable error.");
    }

}
