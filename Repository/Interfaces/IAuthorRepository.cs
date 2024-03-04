using BusinessObjects.Models;

namespace Repositories.Interfaces;

public interface IAuthorRepository
{
    List<Author> GetAll();
    Author? GetById(int id);
    List<Author> SearchAuthors(string keyword);
    Author SaveAuthor(Author author);
    bool DeleteAuthor(int id);
}
