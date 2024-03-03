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
                var account = authService.Regiser(RegisterForm.Email!, RegisterForm.Password!);
                if (account == null)
                {
                    // Error when register new user
                    return Page();
                }

                HttpContext.Session.SetObjectAsJson(SessionUtils.LOGGED_IN_USER_KEY, account);
                return RedirectToPage("/Index");
            }
            else
            {
                return Page();
            }
        }
    }
}
