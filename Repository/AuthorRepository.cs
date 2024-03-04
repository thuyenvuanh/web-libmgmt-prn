using BusinessObjects.Models;
using DataAccessObject;
using Repositories.Interfaces;

namespace Repositories;

public class AuthorRepository : IAuthorRepository
{
    public List<Author> GetAll() => AuthorDAO.Instance.Get();

    public Author? GetById(int id) => AuthorDAO.Instance.GetById(id);

    public List<Author> SearchAuthors(string keyword) => AuthorDAO.Instance.SearchAuthors(keyword);

    public Author? SaveAuthor(Author author) => AuthorDAO.Instance.SaveAuthor(author);

    public bool DeleteAuthor(int id) => AuthorDAO.Instance.Delete(id);
}
