namespace Icarus.Domain.Authorization;

public enum Permission
{
    MemberRead = 0,
    MemberCreate = 1,
    MemberUpdate = 2,
    MemberDelete = 3,
    AccessEverything = int.MaxValue
}
