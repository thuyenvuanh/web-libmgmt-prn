using BusinessObjects.Models;

namespace Services.Interfaces;

public interface IAuthService
{
    Account? Login(string email, string password);
    Account Regiser(string email, string password);
}
