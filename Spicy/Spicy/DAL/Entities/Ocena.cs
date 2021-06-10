using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Spicy.DAL.Entities
{
	class Ocena
	{
        #region Własności
        public sbyte? Id_u { get; set; }
        public sbyte? Id_p { get; set; }
        public bool ocena { get; set; }
        #endregion

        #region Konstruktory
        public Ocena(MySqlDataReader reader)
        {
            Id_u = sbyte.Parse(reader["id_u"].ToString());
            Id_p = sbyte.Parse(reader["id_p"].ToString());
            ocena = bool.Parse(reader["ocena"].ToString());
        }
        #endregion
    }
}
