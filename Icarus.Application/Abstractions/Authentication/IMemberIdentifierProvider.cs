namespace Icarus.Application.Abstractions.Authentication;

public interface IMemberIdentifierProvider
{
    Guid MemberId { get; }
}
