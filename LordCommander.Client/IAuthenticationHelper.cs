using System.Threading.Tasks;

namespace LordCommander.Client
{
    public interface IAuthenticationHelper
    {
        Task Register(string email, string password, string confirmPassword);
        Task<LoginResult> Login(string username, string password);
        void ChangePassword(string oldPassword, string newPassword, string confirmPassword);
    }
}