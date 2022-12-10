using Microsoft.AspNetCore.Mvc;

namespace JsonDiffer.Api.Shared
{
    public class DefaultRouteAttribute : RouteAttribute
    {
        public DefaultRouteAttribute(string template) : base($"{BaseRoute}/{template}") { }

        private const string BaseRoute =  "json-differ/v1";
    }
}
