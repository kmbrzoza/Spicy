using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Spicy.DAL.Entities
{
    class Shop
    {
        #region Properties
        public uint? Id { get; set; }
        public string Name { get; set; }
        #endregion

        #region Constructors
        public Shop(MySqlDataReader reader)
        {
            Id = uint.Parse(reader["id_s"].ToString());
            Name = reader["name"].ToString();
        }

        public Shop(string name)
        {
            Id = null;
            Name = name;
        }
        #endregion
    }
}
