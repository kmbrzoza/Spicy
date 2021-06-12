using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Spicy.DAL.Entities
{
    class User
    {
        #region Properties
        public int Id { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }
        #endregion

        #region Constructors
        public User(MySqlDataReader reader)
        {
            Id = int.Parse(reader["id_u"].ToString());
            Nickname = reader["nickname"].ToString();
            Password = reader["password"].ToString();
        }

        public User(string nickname, string password)
        {
            Id = null;
            Nickname = nickname;
            Password = password;
        }
        #endregion
    }
}
