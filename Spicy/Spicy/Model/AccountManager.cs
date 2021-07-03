using Spicy.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spicy.Model
{
    class AccountManager
    {
        private static AccountManager instance = null;
        public static AccountManager Instance => instance ?? (instance = new AccountManager());
        private AccountManager()
        {
            CurrentUser = null;
        }

        public ObservableCollection<User> Users { get; set; } = new ObservableCollection<User>();
        public User CurrentUser { get; set; }

        public bool UserExists(User user) => Users.Contains(user);

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
                existingUser.Password = "";
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
    }
}
