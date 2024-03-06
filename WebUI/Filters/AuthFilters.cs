using BusinessObjects;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebUI.Utils;

namespace WebUI.Filters
{
    public class AuthFilters : IPageFilter
    {
        private static string[] Non_Authorized_Routes = { "/Login", "/Register", "/Logout" };
        private static string[] Landing_Route = { "/", "/Index" };
        private static Dictionary<ERole, List<string>> Restricted_Routes = new() {
            {ERole.Administrator, new List<string> { "/User"} },
            {ERole.Manager, new List<string>  {"/Authors", "/Books"} },
            {ERole.Customer, new List<string> { "/Browse", "/Bookshelf"} }
        };

        public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        {
        }

        public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            if (Non_Authorized_Routes.Contains(context.HttpContext.Request.Path.Value ?? "") || context.HttpContext.Request.Path.StartsWithSegments("/Errors"))
            {
                return;
            }
            if (!context.HttpContext.Session.IsUserLoggedIn())
            {
                context.Result = new RedirectToPageResult("/Login");
            }
            else
            {
                Account account = context.HttpContext.Session.GetObjectFromJson<Account>(SessionUtils.LOGGED_IN_USER_KEY);
                string path = context.HttpContext.Request.Path.Value ?? "/";
                if (Landing_Route.Contains(path))
                {
                    context.Result = new RedirectToPageResult(Restricted_Routes.GetValueOrDefault((ERole)account.Role)!.First() + "/Index");
                }
                else
                {
                    var restricted = Restricted_Routes.GetValueOrDefault((ERole)account.Role)!;
                    var isForbid = true;
                    foreach(string route in restricted)
                    {
                        if (path.Contains(route))
                        {
                            isForbid = false;
                            break;
                        }
                    }
                    if (isForbid)
                    {
                        context.Result = new RedirectResult("/Errors/403");
                    }
                }
            }
        }

        public void OnPageHandlerSelected(PageHandlerSelectedContext context)
        {
        }
    }
}
