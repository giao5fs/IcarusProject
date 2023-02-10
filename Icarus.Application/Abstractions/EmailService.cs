using Icarus.Domain.Entity;
using System.Diagnostics;

namespace Icarus.Application.Abstractions;

public class EmailService : IEmailService
{
    public async Task<Member> SendRegisteredMemberAsync(Member member, CancellationToken cancellation)
    {
        Debug.WriteLine($"Sent info: {member.Email}");
        await Task.Delay(100);
        return member;
    }
}
