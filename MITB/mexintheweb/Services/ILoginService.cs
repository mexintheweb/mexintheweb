using mexintheweb.Models;

namespace mexintheweb.Services
{
    public interface ILoginService
    {
        Task<string> GetWebtokenByLogin(LoginModel login);
    }
}
