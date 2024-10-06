using MediatR;

namespace LETOS.Share.Abstractions.Shared;


public interface IQuery : IRequest<Result>
{
}


public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{ }
