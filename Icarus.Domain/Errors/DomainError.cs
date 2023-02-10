using Icarus.Domain.Shared;

namespace Icarus.Domain.Errors;

public static class DomainError
{
    public static class Member
    {
        public static readonly Error EmailAlreadyInUse = new Error(
            "Member.EmailAlreadyInUse",
            "Email already in use");

        public static readonly Error InvalidCredentials = new Error(
            "Member.InvalidCredentials",
            "Invalid Credentials");
    }

}
