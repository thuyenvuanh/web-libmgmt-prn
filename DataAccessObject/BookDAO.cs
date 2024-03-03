using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessObject;

public class BookDAO
{
    private readonly LibMgmtContext _dbContext = new();

    private static BookDAO _instance;

    public static BookDAO Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new BookDAO();
            }
            return _instance;
        }
    }

    private BookDAO() { }

    public List<Book> Get() => _dbContext.Books.Include(b => b.AuthorNavigation).ToList();

    public Book Save(Book book)
    {
        if (book.Id != 0)
        {
            _dbContext.Update(book);
        }
        else
        {
            _dbContext.Add(book);
        }
        _dbContext.SaveChanges();
        return book;
    }

    public bool Delete(Book book)
    {
        if (book == null)
        {
            return false;
        }
        _dbContext.Books.Remove(book);
        _dbContext.SaveChanges();
        return true;
    }

    public Book? GetById(int id) => _dbContext.Books.Include(b => b.AuthorNavigation).FirstOrDefault(x => x.Id == id);

    public List<Book> SearchByName(string keyword) => _dbContext.Books.Where(b => b.Name.ToLower().Contains(keyword.ToLower())).ToList();
}
