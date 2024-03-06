using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebUI.Binding;
using Services.Interfaces;

namespace WebUI.Pages.User
{
    public class CreateModel : PageModel
    {
        private readonly IUserService userService;

        public CreateModel(IUserService userService)
        {
            this.userService = userService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public RegisterFormBinding Account { get; set; } = default!;

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid || Account == null)
            {
                return Page();
            }

            userService.SaveAccount(Account.ToAccount());

            return RedirectToPage("./Index");
        }
    }
}
