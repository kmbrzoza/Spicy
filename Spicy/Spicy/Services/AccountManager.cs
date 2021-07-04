using Spicy.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spicy.Services
{
    class AccountManager
    {
        private static AccountManager instance = null;
        public static AccountManager Instance => instance ?? (instance = new AccountManager());
        private AccountManager()
        {
            CurrentUser = null;
            //TODO: get users from DB
            var user1 = new User("test1", "test1") { Id = 1 };
            var user2 = new User("test210987654321", "test2") { Id = 2 };
            Users.Add(user1);
            Users.Add(user2);
        }

        private ObservableCollection<User> Users { get; set; } = new ObservableCollection<User>();
        public User CurrentUser { get; private set; }

        private bool UserExists(User user) => Users.Contains(user);

        public bool RegisterUser(User user)
        {
            if (!UserExists(user))
            {
                // ADD USER TO DB
                Users.Add(user);
                return true;
            }
            return false;
        }

        public bool LoginUser(User user)
        {
            var existingUser = Users.FirstOrDefault(
                u => u.Nickname == user.Nickname
                && u.Password == user.Password);

            if (existingUser != null)
            {
                existingUser.Password = null;
                CurrentUser = existingUser;
                return true;
            }
            return false;
        }
        public bool LogOut()
        {
            CurrentUser = null;
            return true;
        }

        public User GetUserById(uint id)
        {
            var user = Users.FirstOrDefault(u => u.Id == id);
            if (user != null) user.Password = null;
            return user;
        }
    }
}
