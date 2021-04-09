using Microsoft.AspNetCore.Mvc.Filters;
using NLog;

namespace Fun.Api.Helpers
{
    public class ExceptionLogger : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var logger = LogManager.GetCurrentClassLogger();
            logger.Error(context.Exception, "Unhandled exception occurred");
        }
    }
}
