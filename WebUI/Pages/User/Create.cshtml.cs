﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebUI.Binding;
using Repositories.Interfaces;

namespace WebUI.Pages.User
{
    public class CreateModel : PageModel
    {
        private readonly IAccountRepository accountRepository;

        public CreateModel(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
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

            accountRepository.SaveAccount(Account.ToAccount());

            return RedirectToPage("./Index");
        }
    }
}
