using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;
using WebUI.Binding;
using WebUI.Utils;

namespace WebUI.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public RegisterFormBinding RegisterForm { get; set; } = new();

        private readonly IAuthService authService;

        public RegisterModel(IAuthService authService)
        {
            this.authService = authService;
        }

        public IActionResult OnGet()
        {
            return !HttpContext.Session.IsUserLoggedIn() ? Page() : RedirectToPage("/Index");
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var account = authService.Regiser(RegisterForm.Email!, RegisterForm.Password!);
                    HttpContext.Session.SetObjectAsJson(SessionUtils.LOGGED_IN_USER_KEY, account);
                    return RedirectToPage("/Index");
                }
                catch (Exception e)
                {
                    ViewData["ErrorMessage"] = e.Message;
                    return Page();
                }
            }
            else
            {
                ViewData["ErrorMessage"] = "Incorrect email or password";
                return Page();
            }
        }
    }
}
