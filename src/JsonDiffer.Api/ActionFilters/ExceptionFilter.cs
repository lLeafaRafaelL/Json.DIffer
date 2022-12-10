using JsonDiffer.Shared.Correlations;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using JsonDiffer.Shared.Logging;

namespace JsonDiffer.Api.ActionFilters
{
    internal class ExceptionFilter : IExceptionFilter
    {
        private readonly ICustomLogger _customLogger;
        private readonly ICorrelationService _correlationService;

        public ExceptionFilter(ICustomLogger customLogger, ICorrelationService correlationService)
        {
            _customLogger = customLogger ?? throw new ArgumentNullException(nameof(customLogger));
            _correlationService = correlationService ?? throw new ArgumentNullException(nameof(correlationService));
        }

        public void OnException(ExceptionContext context)
        {
            //TODO: IMPLEMENTAR SISTEMA DE LOG
            //_customLogger.LogInfo(_correlationService.ObterCorrelationId(), context.Exception);
        }
    }
}
