namespace Icarus.Application.Members.RegisterMember;

public record RegisterMemberRequest(string Email, string Password, string ConfirmPassword, string FirstName, string LastName);
