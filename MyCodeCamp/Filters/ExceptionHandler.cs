using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using MyCodeCamp.Controllers;

namespace MyCodeCamp.Filters
{
    public class ExceptionHandler : ExceptionFilterAttribute
    {

        //public static class ApplicationLogging
        //{
        //    public static ILoggerFactory LoggerFactory {get;} = new LoggerFactory();
        //    public static ILogger CreateLogger<T>() =>
        //        LoggerFactory.CreateLogger<T>();
        //}
        //public class CustomExceptionFilterAttribute : ExceptionFilterAttribute { ILogger Logger { get; } =
        //        ApplicationLogging.CreateLogger<CustomExceptionFilterAttribute>();// to tell where we log

        //    public override void OnException(ExceptionContext context) {
  
        //        using (Logger.BeginScopeImpl(
        //            $"=>{ nameof(OnException) }")) // to tell which method we log
        //        {
        //            Logger.LogInformation("Log Message"); // to tell what exception we log
        //        }
        //    }
        //}

        public override void OnException(ExceptionContext context)
        {
            if (context == null) return;  // not much we can do here

            context.ExceptionHandled = true;

            context.Result = context.Exception == null ? 
                new BadRequestObjectResult("error") : 
                new BadRequestObjectResult(context.Exception.Message);

        }
    }
}