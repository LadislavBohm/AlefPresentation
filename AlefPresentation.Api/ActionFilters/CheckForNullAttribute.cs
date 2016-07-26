using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace AlefPresentation.Api.ActionFilters
{
    public class CheckForNullAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var nullArguments = actionContext.ActionArguments.Where(v => v.Value == null).ToList();
            if (nullArguments.Any())
            {
                actionContext.Response = actionContext.Request
                    .CreateResponse(HttpStatusCode.BadRequest,$"Arguments are null: {string.Join(", ",nullArguments.Select(a => a.Key))}");
            }
        }
    }
}