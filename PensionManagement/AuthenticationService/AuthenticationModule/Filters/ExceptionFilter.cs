using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace AuthenticationModule.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<ExceptionFilter> _logger;
        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }

      
        public override void OnException(ExceptionContext context)
        {
            var statusCode = HttpStatusCode.InternalServerError;

            context.HttpContext.Response.ContentType = StaticData.ContentType;
            context.HttpContext.Response.StatusCode = (int)statusCode;
            context.Result = new JsonResult(new
            {
                error = new[] { context.Exception.Message },
                stackTrace = context.Exception.StackTrace
            });

            _logger.LogError(context.Exception.Message + " - " + context.Exception.StackTrace);
        }

        

    
    }
}