using Icarus.Domain.Shared;
using MediatR;

namespace Icarus.Application.Abstractions.Messaging
{
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>
    {
    }
}
