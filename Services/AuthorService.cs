using BusinessObjects.Models;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository repository;
    
    public AuthorService(IAuthorRepository repository)
    {
        this.repository = repository;
    }

    public bool DeleteAuthor(int id)
    {
        return repository.DeleteAuthor(id);
    }

    public List<Author> GetAll()
    {
        return repository.GetAll();
    }

    public Author? GetAuthor(int id)
    {
        return repository.GetById(id);
    }

    public Author? SaveAuthor(Author author)
    {
        return repository.SaveAuthor(author);
    }
}
