using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects.Models;
using Repositories.Interfaces;

namespace WebUI.Pages.User
{
    public class IndexModel : PageModel
    {
        private readonly IAccountRepository accountRepository;
        public IndexModel(IAccountRepository accountRepository)
        {
           this.accountRepository = accountRepository;
        }

        public IList<Account> Account { get;set; } = default!;

        public void OnGet()
        {
            Account = accountRepository.GetAll();
        }
    }
}
