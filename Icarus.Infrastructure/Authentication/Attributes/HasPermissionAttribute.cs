using Icarus.Domain.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Icarus.Infrastructure.Authentication.Attributes;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false)]
public sealed class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(PermissionEnum permission) 
        : base(policy: permission.ToString())
    {

    }
}
