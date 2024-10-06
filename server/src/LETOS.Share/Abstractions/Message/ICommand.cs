using MediatR;

namespace LETOS.Share.Abstractions.Shared;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{

}
