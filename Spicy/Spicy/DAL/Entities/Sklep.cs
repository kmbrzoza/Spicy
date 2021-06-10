using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Spicy.DAL.Entities
{
    class Sklep
    {
        #region Własności
        public sbyte? Id { get; set; }
        public string nazwa { get; set; }
        #endregion

        #region Konstruktory
        public Sklep(MySqlDataReader reader)
        {
            Id = sbyte.Parse(reader["id_s"].ToString());
            nazwa = reader["nazwa"].ToString();
        }
        #endregion
    }
}
