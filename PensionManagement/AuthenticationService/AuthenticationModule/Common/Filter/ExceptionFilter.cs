using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace AuthenticationModule.Common.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        private ILogger<ExceptionFilter> _logger;
        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }

      
        public override void OnException(ExceptionContext context)
        {
            var statusCode = HttpStatusCode.InternalServerError;

            context.HttpContext.Response.ContentType = StaticData.CONTENTTYPE;
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