using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace WebUI.Pages.User
{
    public class DeleteModel : PageModel
    {
        private readonly IUserService userService;

        public DeleteModel(IUserService userService)
        {
            this.userService = userService;
        }

        public IActionResult OnGet(int? id)
        {
            return RedirectToPage("/User/Index");
        }

        public IActionResult OnPost(int? id)
        {
            if (id == null)
            {
                return RedirectToPage("Errors/404");
            }

            var account = userService.GetAccount((int)id);

            if (account != null)
            {
                userService.DeleteAccount(account);
            }

            return RedirectToPage("./Index");
        }
    }
}
