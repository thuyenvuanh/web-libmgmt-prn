using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.Interfaces;

namespace WebUI.Pages.Books
{
    public class DeleteModel : PageModel
    {
        public readonly IBookRepository bookRepository;

        public DeleteModel(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        public IActionResult OnPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var book = bookRepository.GetById((int)id);

            if (book != null)
            {
                bookRepository.Delete(book);
            }

            return RedirectToPage("./Index");
        }
    }
}
