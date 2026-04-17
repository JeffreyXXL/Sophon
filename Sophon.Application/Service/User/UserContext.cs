using Sophon.Infrastructure;

namespace Sophon.Application
{
    public class UserContext : IUserContext
    {
        public string CurrentUser { get; set; }
        public UserLevel CurrentLevel { get; set; } = UserLevel.None;

        public bool IsLoggedIn { get; set; } = false;

        public void Login(string userName)
        {
            CurrentUser = userName;
        }

        public void Logout()
        {
            CurrentUser = string.Empty;
        }
    }
}