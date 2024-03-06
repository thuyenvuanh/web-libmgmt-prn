using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects.Models;
using Services.Interfaces;

namespace WebUI.Pages.User
{
    public class IndexModel : PageModel
    {
        private readonly IUserService userService;
        public IndexModel(IUserService userService)
        {
            this.userService = userService;
        }

        public IList<Account> Account { get;set; } = default!;

        public void OnGet()
        {
            Account = userService.GetAccounts();
        }
    }
}
