using BusinessObjects.Models;

namespace DataAccessObject;

public  class AuthorDAO
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
}
