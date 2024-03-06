using BusinessObjects.Models;

namespace Services.Interfaces;

public interface IAuthorService
{
    Author? SaveAuthor(Author author);
    Author? GetAuthor(int id);
    List<Author> GetAll();
    bool DeleteAuthor(int id);
}
