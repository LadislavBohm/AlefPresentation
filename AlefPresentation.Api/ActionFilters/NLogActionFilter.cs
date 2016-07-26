using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using NLog;

namespace AlefPresentation.Api.ActionFilters
{
    public class NLogActionFilter : ActionFilterAttribute
    {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            base.OnActionExecuting(actionContext);

            _logger.Info($"Executing action {actionContext.ActionDescriptor.ActionName} on controller {actionContext.ControllerContext.ControllerDescriptor.ControllerName}");

            if (_logger.IsDebugEnabled)
            {
                _logger.Debug($"Arguments: {string.Join(" | ",actionContext.ActionArguments.Select(a => $"{a.Key} - {a.Value}"))}");
            }
        }
    }
}