using Icarus.Application.Abstractions.Messaging;
using Icarus.Domain.Errors;
using Icarus.Domain.Repositories;
using Icarus.Domain.Shared;
using MediatR;

namespace Icarus.Application.Members.Queries;

internal sealed class GetMemberByIdQueryHandler
    : IQueryHandler<GetMemberByIdQuery, Result<MemberResponse>>
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
            return Result.Failure<MemberResponse>(DomainError.ErrorMember.MemberIdIsRequired);
        }

        return new MemberResponse(member.Id, member.Email);
    }
}
