using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects.Models;
using Services.Interfaces;

namespace WebUI.Pages.Authors
{
    public class CreateModel : PageModel
    {
        private readonly IAuthorService authorService;

        public CreateModel(IAuthorService authorService)
        {
            this.authorService = authorService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Author Author { get; set; } = default!;

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid || Author == null)
            {
                return Page();
            }

            authorService.SaveAuthor(Author);

            return RedirectToPage("./Index");
        }
    }
}
