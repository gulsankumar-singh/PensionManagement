using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using log4net;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AuthenticationModule.Services.Filter
{
    public class LogFilterAttribute : ExceptionFilterAttribute
    {
        private ILog _logger;
        public LogFilterAttribute()
        {
            _logger = LogManager.GetLogger(typeof(LogFilterAttribute));
        }

        public override void OnException(ExceptionContext context)
        {
            _logger.Error(context.Exception.Message + " - " + context.Exception.StackTrace);
        }
    }
}