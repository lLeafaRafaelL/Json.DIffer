using MediatR;

namespace JsonDiffer.Api.Shared
{
    public class BaseController
    {
        protected BaseController(IMediator mediator) =>
            Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        protected IMediator Mediator { get; init; }
    }
}
