using Icarus.Application.Members.RegisterMember;
using Icarus.Application.Members.Login;
using Icarus.Application.Members.Queries;
using Icarus.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Icarus.Presentation.Controllers;

[Route("api/member")]
public sealed class MemberController : ApiController
{
    public MemberController(ISender sender) : base(sender)
    {
    }

    //[Authorize]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetMemberById(Guid id, CancellationToken cancellation)
    {
        var query = new GetMemberByIdQuery(id);

        Result<MemberResponse> response = await _sender.Send(query, cancellation);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginMember(
        [FromBody] LoginRequest request,
        CancellationToken cancellationToken)
    {
        var command = new LoginCommand(request.Email);

        Result<string> token = await _sender.Send(command, cancellationToken);
        if (token.IsFailure)
        {
            return BadRequest(token);
            //return HandleFailure(token);
        }

        return Ok(token.Value);
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterMember(
        [FromBody] RegisterMemberRequest request,
        CancellationToken cancellationToken)
    {
        var command = new RegisterMemberCommand(
            request.Email,
            request.FirstName,
            request.LastName);

        Result<Guid> result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }
        //return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);

        return CreatedAtAction(nameof(GetMemberById), new { id = result.Value }, result.Value);
    }
}
