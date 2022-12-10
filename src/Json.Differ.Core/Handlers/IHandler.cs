using MediatR;

namespace Json.Differ.Core.Handlers
{
    public interface IHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    {
    }
}