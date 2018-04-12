using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

using MyCodeCamp.Helpers;

namespace MyCodeCamp.Controllers
{
    public abstract class BaseController<T> : Controller
    { 
        protected ILogger<T> Logger;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            context.HttpContext.Items[Constants.UrlHelper] = this.Url;

            Logger = LogHelper.CreateLogger<T>();
        }
    }
}