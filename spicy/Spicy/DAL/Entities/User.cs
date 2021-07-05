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
        public uint? Id { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }
        #endregion

        #region Constructors
        public User(MySqlDataReader reader)
        {
            Id = uint.Parse(reader["id_user"].ToString());
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

        #region Methods
        public override bool Equals(object obj)
        {
            User user = obj as User;
            if (user is null) return false;
            if (Nickname.ToLower() != user.Nickname.ToLower()) return false;

            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public string ToInsert()
        {
            return $"('{Nickname}', '{Password}')";
        }

        #endregion
    }
}
