using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace Spicy.DAL.Repositories
{
    using Entities;
    class UserRepo
    {
        #region QUERIES
        private const string GET_USERS = "SELECT * FROM user";
        private const string GET_USERS_WITH_USERNAME = "SELECT * FROM user WHERE name =";
        private const string ADD_USER = "INSERT INTO `user`(`name`, `password`) VALUES ";
        #endregion

        #region METHODS
        public static bool AddUser(User user)
        {
            bool status = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{ADD_USER} {user.ToInsert()}", connection);
                connection.Open();
                var id = command.ExecuteNonQuery();
                status = true;
                user.Id = (uint)command.LastInsertedId;
                connection.Close();
            }
            return status;
        }
        #endregion

    }
}
