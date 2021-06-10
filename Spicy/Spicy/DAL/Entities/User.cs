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
        #region Własności
        public sbyte? Id { get; set; }
        public string nick { get; set; }
        public string haslo { get; set; }
        #endregion

        #region Konstruktory
        public User(MySqlDataReader reader)
        {
            Id = sbyte.Parse(reader["id_u"].ToString());
            nick = reader["nick"].ToString();
            haslo = reader["haslo"].ToString();
        }
        #endregion
    }
}
