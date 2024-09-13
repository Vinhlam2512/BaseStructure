using MediatR;

namespace ERP.Share.Abstractions.Shared;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{

}
