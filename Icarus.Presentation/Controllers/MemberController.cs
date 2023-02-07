using Icarus.Application.Members.CreateMember;
using Icarus.Application.Members.Queries;
using Icarus.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace Icarus.Presentation.Controllers
{
    public sealed class MemberController : ApiController
    {
        public MemberController(ISender sender) : base(sender)
        {
        }
        [HttpPost]
        public async Task<IActionResult> RegisterMember(CancellationToken cancellationToken)
        {
            var command = new CreateMemberCommand(
                "giaonx@pixelz.com",
                "Giao",
                "Nguyen");
            var result = await _sender.Send(command, cancellationToken);

            return result.IsSuccess ? Ok() : BadRequest(result.Error);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMemberById(Guid id, CancellationToken cancellation)
        {
            var query = new GetMemberByIdQuery(id);

            Result<MemberResponse> response = await _sender.Send(query, cancellation);

            return Response.IsSuccess
        }
    }
}
