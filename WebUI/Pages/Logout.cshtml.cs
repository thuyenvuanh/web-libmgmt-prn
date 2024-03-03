using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebUI.Utils;

namespace WebUI.Pages
{
    public class LogoutModel : PageModel
    {
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            HttpContext.Session.Remove(SessionUtils.LOGGED_IN_USER_KEY);
            return RedirectToPage("/Login");
        }
    }
}
