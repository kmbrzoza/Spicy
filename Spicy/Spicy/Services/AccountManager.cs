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
            currentUser = null;
            //TODO: get users from DB
            var user1 = new User("test1", "test1") { Id = 1 };
            var user2 = new User("test210987654321", "test2") { Id = 2 };
            var user3 = new User("test2", "test2") { Id = 3 };
            Users.Add(user1);
            Users.Add(user2);
            Users.Add(user3);
        }

        private ObservableCollection<User> Users { get; set; } = new ObservableCollection<User>();
        private User currentUser { get; set; }
        public User CurrentUser { get => new User() { Id = currentUser.Id, Nickname = currentUser.Nickname }; }

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
                currentUser = existingUser;
                return true;
            }
            return false;
        }
        public bool LogOut()
        {
            currentUser = null;
            return true;
        }

        public User GetUserById(uint id)
        {
            User userToReturn = null;
            var user = Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
                userToReturn = new User() { Id = user.Id, Nickname = user.Nickname };
            
            return userToReturn;
        }
    }
}
