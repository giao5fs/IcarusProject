namespace Icarus.Domain.Services;

public interface IPasswordHashChecker
{
    bool IsHashPasswordMatch(string passwordHash, string originPassword);
}
