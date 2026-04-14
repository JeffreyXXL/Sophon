using Sophon.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sophon.Core
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
