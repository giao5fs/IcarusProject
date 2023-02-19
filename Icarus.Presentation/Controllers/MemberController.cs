using Icarus.Application.Members.Queries;
using Icarus.Domain.Enums;
using Icarus.Domain.Shared;
using Icarus.Infrastructure.Authentication.Attributes;
using Icarus.Presentation.Routes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Icarus.Presentation.Controllers;

public sealed class MemberController : ApiController
{
    public MemberController(ISender sender) : base(sender)
    {
    }

    [HttpGet(ApiRoutes.Member.GetMemberById)]
    [HasPermission(PermissionEnum.AccessEverything)]
    public async Task<IActionResult> GetMemberById(Guid id, CancellationToken cancellation)
    {
        var query = new GetMemberByIdQuery(id);

        Result<MemberResponse> response = await _sender.Send(query, cancellation);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
    }
}
