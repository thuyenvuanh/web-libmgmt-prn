using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects.Models;
using Repositories.Interfaces;

namespace WebUI.Pages.Authors
{
    public class IndexModel : PageModel
    {
        private readonly IAuthorRepository authorRepository;

        public IndexModel(IAuthorRepository authorRepository)
        {
            this.authorRepository = authorRepository;
        }

        public IList<Author> Author { get;set; } = default!;

        public void OnGet()
        {
            Author = authorRepository.GetAll();
        }
    }
}
