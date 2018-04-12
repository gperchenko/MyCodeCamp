using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using MyCodeCamp.Helpers;

namespace MyCodeCamp.Filters
{
    public class ExceptionHandler : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context == null) return;  // not much we can do here

            context.ExceptionHandled = true;
            var logger = LogHelper.CreateLogger<ExceptionHandler>();

            if (context.Exception != null)
            {
                logger.LogError(context.Exception.Message);
                logger.LogError(context.Exception.StackTrace);
                context.Result = new BadRequestObjectResult(context.Exception.Message);
            }
            else
            {
                logger.LogError("Unknown exception");
                context.Result = new BadRequestObjectResult("Unknown exception");
            }
        }
    }
}