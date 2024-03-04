using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects.Models;
using Repositories.Interfaces;

namespace WebUI.Pages.Authors
{
    public class DeleteModel : PageModel
    {
        private readonly IAuthorRepository _authorRepository;

        public DeleteModel(IAuthorRepository authorRepository)
        {
            this._authorRepository = authorRepository;
        }

        [BindProperty]
        public Author Author { get; set; } = default!;


        public IActionResult OnPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var author = _authorRepository.GetById((int)id);

            if (author != null)
            {
                _authorRepository.DeleteAuthor((int)id);
            }

            return RedirectToPage("./Index");
        }
    }
}
