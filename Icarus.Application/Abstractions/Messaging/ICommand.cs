using Icarus.Domain.Shared;
using MediatR;

namespace Icarus.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}
