using BusinessObjects.Models;

namespace Services.Interfaces;

public interface IBookService
{
    Book? GetBook(int id);
    Book? SaveBook(Book book);
    List<Book> GetAllBooks();
    bool DeleteBook(Book book);
}
