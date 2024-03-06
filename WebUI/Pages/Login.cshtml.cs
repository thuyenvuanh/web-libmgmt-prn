using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;
using WebUI.Binding;
using WebUI.Utils;

namespace WebUI.Pages;

public class LoginModel : PageModel
{
    [BindProperty]
    public LoginFormBinding LoginForm { get; set; } = new();

    private readonly IAuthService authService;

    public LoginModel(IAuthService authService)
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
            var account = authService.Login(LoginForm.Email!, LoginForm.Password!);
            if (account == null)
            {
                ViewData["ErrorMessage"] = "Incorrect email or password";
                return Page();
            } 
            else
            {
                HttpContext.Session.SetObjectAsJson(SessionUtils.LOGGED_IN_USER_KEY, account);
                return RedirectToPage("/Index");
            }
        }
        return Page();
    }
}
