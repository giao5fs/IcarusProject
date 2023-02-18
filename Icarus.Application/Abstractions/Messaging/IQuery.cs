using Icarus.Domain.Shared;
using MediatR;

namespace Icarus.Application.Abstractions.Messaging
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}
