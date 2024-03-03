using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebUI.Utils;

namespace WebUI.Filters
{
    public class AuthFilters : IPageFilter
    {
        private static string[] Non_Authorized_Routes = { "/login", "/register" };

        public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        {
        }

        public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            if (Non_Authorized_Routes.Contains(context.HttpContext.Request.Path.Value?.ToLower() ?? ""))
            {
                return;
            }
            if (!context.HttpContext.Session.IsUserLoggedIn())
            {
                context.Result = new RedirectToPageResult("/Login");
            }
        }

        public void OnPageHandlerSelected(PageHandlerSelectedContext context)
        {
        }
    }
}
