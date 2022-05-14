using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using ProcessPensionModule.Utility;

namespace ProcessPensionModule.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        //private ILog _logger;
        private ILogger<ExceptionFilter> _logger;
        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;//LogManager.GetLogger(typeof(ExceptionFilter));
        }

      
        public override void OnException(ExceptionContext context)
        {
            var statusCode = HttpStatusCode.InternalServerError;

            context.HttpContext.Response.ContentType = StaticData.CONTENT_TYPE;
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