using Icarus.Domain.ValueObjects;

namespace Icarus.Application.Abstractions.Cryptography;

public interface IPasswordHasher
{
    string HashPassword(Password password);
}
