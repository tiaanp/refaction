using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;

namespace refactor_me.Exception
{
    [type:
            AttributeUsage(
                AttributeTargets.Class | AttributeTargets.Method,
                Inherited = true,
                AllowMultiple = true)
        ]
    public class HandleApiException : ExceptionFilterAttribute
    {

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            //Impliment Server side logging
            base.OnException(actionExecutedContext);
        }
    }
}