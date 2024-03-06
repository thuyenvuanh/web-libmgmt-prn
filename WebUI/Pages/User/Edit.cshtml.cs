using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;
using WebUI.Binding;
using System.ComponentModel.DataAnnotations;
using Services.Interfaces;

namespace WebUI.Pages.User
{
    public class EditModel : PageModel
    {
        private readonly IUserService userService;

        public EditModel(IUserService userService)
        {
            this.userService = userService;
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
                return RedirectToPage("Errors/404");
            }

            var account =  userService.GetAccount((int)id);


            if (account == null)
            {
                return RedirectToPage("Errors/404");
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

            var currentAccount = userService.GetAccount(Account.Id);
            if (currentAccount == null)
            {
                return RedirectToPage("Errors/404");
            }
            if (!currentAccount.Password.Equals(OldPassword))
            {
                OldPasswordErrorMessage = "Password incorrect";
                return Page();
            }

            try
            {
                currentAccount.UpdateWith(Account.ToAccount());
                userService.SaveAccount(currentAccount);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(Account.Id))
                {
                    return RedirectToPage("Errors/404");
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
            return userService.GetAccount(id) != null;
        }
    }
}
