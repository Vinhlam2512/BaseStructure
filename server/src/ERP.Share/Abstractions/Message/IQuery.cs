using MediatR;

namespace ERP.Share.Abstractions.Shared;


public interface IQuery : IRequest<Result>
{
}


public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{ }
