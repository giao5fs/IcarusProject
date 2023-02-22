using Icarus.Application.Members.Commands.Login;
using Icarus.Application.Members.Login;
using Icarus.Application.Members.RegisterMember;
using Icarus.Domain.Shared;
using Icarus.Presentation.Routes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Icarus.Presentation.Controllers;

[AllowAnonymous]
public class AccountController : ApiController
{
    public AccountController(ISender sender) : base(sender)
    {
    }

    [HttpPost(ApiRoutes.Authentication.Login)]
    public async Task<IActionResult> LoginMember(
        [FromBody] LoginRequest request,
        CancellationToken cancellationToken)
    {
        var command = new LoginCommand(request.Email, request.Password);

        Result<TokenResponse> token = await _sender.Send(command, cancellationToken);

        if (token.IsFailure)
        {
            return HandleFailure(token);
        }

        return token.IsSuccess ? Ok(token.Value) : BadRequest(token.Value);
    }

    [HttpPost(ApiRoutes.Authentication.Register)]
    public async Task<IActionResult> RegisterMember(
        [FromBody] RegisterMemberRequest request,
        CancellationToken cancellationToken)
    {
        var command = new RegisterMemberCommand(
            request.Email,
            request.Password,
            request.ConfirmPassword,
            request.FirstName,
            request.LastName
            );

        Result<Guid> result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }
}
