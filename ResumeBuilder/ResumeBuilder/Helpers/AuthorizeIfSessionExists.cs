using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ResumeBuilder.Helpers
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class AuthorizeIfSessionExistsAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var sessionUserID = httpContext.Session["UserID"];
            
            if (sessionUserID != null)
            {
                return true;
            }
            return false;
        }
    }
}
        
        