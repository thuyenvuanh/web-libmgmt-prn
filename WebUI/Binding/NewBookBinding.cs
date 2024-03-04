using BusinessObjects.Models;
using System.ComponentModel.DataAnnotations;

namespace WebUI.Binding
{
    public class NewBookBinding
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name = "ISBN Number")]
        public string Isbn { get; set; }
        [Required]
        [Display(Name = "Publised Date"), DataType(DataType.Date)]
        public DateTime PublishedDate { get; set; }
        [Required]
        public string Author { get; set; }

        public static NewBookBinding FromBook(Book book)
        {
            return new()
            {
                Id = book.Id,
                Name = book.Name,
                Isbn = book.Isbn,
                PublishedDate = book.PublishedDate,
                Author = $"{book.Author}. {book.AuthorNavigation.Name}"
            };
        }

        public Book ToBook()
        {
            var book = new Book();
            book.Id = Id;
            book.Name = Name;
            book.Isbn = Isbn;
            book.PublishedDate = PublishedDate;
            book.Author = int.Parse(Author.Split('.')[0]);
            return book;
        }
    }
}
