using BusinessObjects.Models;

namespace DataAccessObject;

public class AuthorDAO
{
    private readonly LibMgmtContext _dbContext = new();

    private static AuthorDAO _instance;

    public static AuthorDAO Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new AuthorDAO();
            }
            return _instance;
        }
    }
    private AuthorDAO() { }

    public List<Author> Get() => _dbContext.Authors.ToList();

    public Author? GetById(int id) => _dbContext.Authors.SingleOrDefault(x => x.Id == id);

    public List<Author> SearchAuthors(string keyword) => _dbContext.Authors.Where(a => a.Name.ToLower().Contains(keyword.ToLower())).ToList();

    public Author SaveAuthor(Author author)
    {
        if (author.Id == 0)
        {
            _dbContext.Authors.Add(author);
        }
        else
        {
            _dbContext.Authors.Update(author);
        }
        _dbContext.SaveChanges();
        return author;
    }

    public bool Delete(int id) { 
        var author = _dbContext.Authors.SingleOrDefault(a => a.Id == id);
        if (author != null)
        {
            _dbContext.Authors.Remove(author);
            _dbContext.SaveChanges();
            return true;
        }
        return false;
    }
}
