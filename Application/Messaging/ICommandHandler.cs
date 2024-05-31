using MediatR;
using SharedKernel;

namespace Application.Messaging;

public interface ICommandHandler<in TCommand>
    where TCommand : ICommand
{
}

public interface ICommanHandler<TCommand, TResponse> 
    : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse> 
{
}
