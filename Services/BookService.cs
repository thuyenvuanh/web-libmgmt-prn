using BusinessObjects.Models;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services;

public class BookService : IBookService
{
    private readonly IBookRepository bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        this.bookRepository = bookRepository;
    }

    public bool DeleteBook(Book book)
    {
        return bookRepository.Delete(book);
    }

    public List<Book> GetAllBooks()
    {
        return bookRepository.GetAll();
    }

    public Book? GetBook(int id)
    {
        return bookRepository.GetById(id);
    }

    public Book? SaveBook(Book book)
    {
        return bookRepository.Save(book);
    }
}
