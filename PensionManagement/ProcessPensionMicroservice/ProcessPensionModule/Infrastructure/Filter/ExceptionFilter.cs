using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace ProcessPensionModule.Infrastructure.Filter
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        private ILogger<ExceptionFilter> _logger;

        /// <summary>
        /// ExceptionFilter constructor
        /// </summary>
        /// <param name="logger"></param>
        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// OnException method for handling if exception occur
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(ExceptionContext context)
        {
            var statusCode = HttpStatusCode.InternalServerError;

            context.HttpContext.Response.ContentType = "application/json";
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