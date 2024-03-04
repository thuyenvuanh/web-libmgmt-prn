using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;
using WebUI.Binding;
using System.ComponentModel.DataAnnotations;

namespace WebUI.Pages.User
{
    public class EditModel : PageModel
    {
        private readonly IAccountRepository accountRepository;

        public EditModel(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        [BindProperty]
        public RegisterFormBinding Account { get; set; } = default!;

        [BindProperty]
        [Display(Name = "Old Password")]
        [Required, MinLength(6), DataType(DataType.Password),]
        public string OldPassword { get; set; } = string.Empty;

        [BindProperty]
        public string? OldPasswordErrorMessage { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account =  accountRepository.GetByID((int)id);


            if (account == null)
            {
                return NotFound();
            }
            Account = RegisterFormBinding.FromAccount(account);
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var currentAccount = accountRepository.GetByID(Account.Id);
            if (currentAccount == null)
            {
                return NotFound();
            }
            if (!currentAccount.Password.Equals(OldPassword))
            {
                OldPasswordErrorMessage = "Password incorrect";
                return Page();
            }

            try
            {
                currentAccount.UpdateWith(Account.ToAccount());
                accountRepository.SaveAccount(currentAccount);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(Account.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool AccountExists(int id)
        {
            return accountRepository.GetByID(id) != null;
        }
    }
}
