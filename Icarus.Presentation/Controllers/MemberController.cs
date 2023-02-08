using Icarus.Application.Members.CreateMember;
using Icarus.Application.Members.Login;
using Icarus.Application.Members.Queries;
using Icarus.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Icarus.Presentation.Controllers
{
    public sealed class MemberController : ApiController
    {
        public MemberController(ISender sender) : base(sender)
        {
        }

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

        [HttpPost]
        public async Task<IActionResult> RegisterMember(
            [FromBody] RegisterMemberRequest request,
            CancellationToken cancellationToken)
        {
            var command = new CreateMemberCommand(
                request.Email,
                request.FirstName,
                request.LastName);

            Result<Guid> result = await _sender.Send(command, cancellationToken);

            return result.IsSuccess ? Ok() : BadRequest(result.Error);
        }

        [Authorize]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetMemberById(Guid id, CancellationToken cancellation)
        {
            var query = new GetMemberByIdQuery(id);

            Result<MemberResponse> response = await _sender.Send(query, cancellation);

            return response.IsSuccess ? Ok() : NotFound(response.Error);
        }   
    }
}
