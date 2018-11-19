using System.Web;
using System.Web.Mvc;
using Hospital.Models.Account;
using Microsoft.AspNet.Identity.Owin;

namespace Hospital.Infrastructure
{
    public static class IdentityHelpers
    {
        public static MvcHtmlString GetUserName(this HtmlHelper html, string id)
        {
            string result = "";
            if (!string.IsNullOrEmpty(id))
            {
                ApplicationUserManager mgr = HttpContext.Current
                    .GetOwinContext().GetUserManager<ApplicationUserManager>();
                result = mgr.FindByIdAsync(id).Result.UserName;
            }
            return new MvcHtmlString(result);
        }
    }
}