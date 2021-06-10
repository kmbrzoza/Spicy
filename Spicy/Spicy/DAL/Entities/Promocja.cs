using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Spicy.DAL.Entities
{
	class Promocja
	{
        #region Własności
        public sbyte? Id { get; set; }
        public string nazwa { get; set; }
        public string opis { get; set; }
        public string link { get; set; }
        public DateTime data_rozp { get; set; }
        public DateTime data_zako { get; set; }
        #endregion

        #region Konstruktory
        public Promocja(MySqlDataReader reader)
        {
            Id = sbyte.Parse(reader["id_p"].ToString());
            nazwa = reader["nazwa"].ToString();
            opis = reader["opis"].ToString();
            link = reader["link"].ToString();
            data_rozp = DateTime.Parse(reader["data_rozp"].ToString());
            data_zako = DateTime.Parse(reader["data_zako"].ToString());
        }
        #endregion
    }
}
