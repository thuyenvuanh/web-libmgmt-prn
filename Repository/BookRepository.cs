using BusinessObjects.Models;
using DataAccessObject;
using Repositories.Interfaces;

namespace Repositories;

public class BookRepository : IBookRepository
{
    public bool Delete(Book book) => BookDAO.Instance.Delete(book);

    public List<Book> GetAll() => BookDAO.Instance.Get();

    public Book? GetById(int id) => BookDAO.Instance.GetById(id);

    public Book Save(Book book) => BookDAO.Instance.Save(book);

    public List<Book> SearchByName(string keyword) => BookDAO.Instance.SearchByName(keyword);
}
