using Icarus.Application.Abstractions.Messaging;
using Icarus.Domain.Repositories;
using Icarus.Domain.Shared;

namespace Icarus.Application.Members.Queries;

internal sealed class GetMemberByIdQueryHandler
    : IQueryHandler<GetMemberByIdQuery, MemberResponse>
{
    private readonly IMemberRepository _memberRepository;

    public GetMemberByIdQueryHandler(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public async Task<Result<MemberResponse>> Handle(GetMemberByIdQuery request, CancellationToken cancellationToken)
    {
        var member = await _memberRepository.GetByIdAsync(
            request.memberId,
            cancellationToken);

        if (member is null)
        {
            return Result.Failure<MemberResponse>(
                new Error(
                    "Member.NotFound",
                    $"The number with Id" +
                    $"was not found"
                    ));
        }

        return new MemberResponse(member.Id,member.Email);
    }
}
