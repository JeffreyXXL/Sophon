using Sophon.Infrastructure;

namespace Sophon.Application
{
    public interface IUserContext
    {
        string CurrentUser { get; set; }
        UserLevel CurrentLevel { get; set; }
        bool IsLoggedIn { get; set; }

        void Login(string userName);

        void Logout();
    }
}