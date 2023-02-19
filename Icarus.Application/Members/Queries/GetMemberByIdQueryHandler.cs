using Icarus.Application.Abstractions.Authentication;
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
    private readonly IMemberIdentifierProvider _memberIdentifierProvider;

    public GetMemberByIdQueryHandler(IMemberRepository memberRepository, IMemberIdentifierProvider memberIdentifierProvider)
    {
        _memberRepository = memberRepository;
        _memberIdentifierProvider = memberIdentifierProvider;
    }

    public async Task<Result<MemberResponse>> Handle(GetMemberByIdQuery request, CancellationToken cancellationToken)
    {
        if (request.memberId != _memberIdentifierProvider.MemberId)
        {
            return Result.Failure<MemberResponse>(DomainError.ErrorAuthentication.UnAuthorized);
        }
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
