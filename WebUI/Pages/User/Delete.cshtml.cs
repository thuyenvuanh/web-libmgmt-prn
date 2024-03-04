using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects.Models;
using Repositories.Interfaces;

namespace WebUI.Pages.User
{
    public class DeleteModel : PageModel
    {
        private readonly IAccountRepository accountRepository;

        public DeleteModel(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        public IActionResult OnGet(int? id)
        {
            return RedirectToPage("/User/Index");
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = accountRepository.GetByID((int)id);

            if (account != null)
            {
                accountRepository.Delete(account);
            }

            return RedirectToPage("./Index");
        }
    }
}
