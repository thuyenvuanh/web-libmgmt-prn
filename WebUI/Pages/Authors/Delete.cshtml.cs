using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects.Models;
using Repositories.Interfaces;
using Services.Interfaces;

namespace WebUI.Pages.Authors
{
    public class DeleteModel : PageModel
    {
        private readonly IAuthorService authorService;

        public DeleteModel(IAuthorService authorService)
        {
            this.authorService = authorService;
        }

        [BindProperty]
        public Author Author { get; set; } = default!;


        public IActionResult OnPost(int? id)
        {
            if (id == null)
            {
                return RedirectToPage("Errors/404");
            }
            var author = authorService.GetAuthor((int)id);

            if (author != null)
            {
                authorService.DeleteAuthor((int)id);
            }

            return RedirectToPage("./Index");
        }
    }
}
