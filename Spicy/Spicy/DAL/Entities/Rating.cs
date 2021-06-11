using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Spicy.DAL.Entities
{
	class Rating
	{
        #region Properties
        public int Id_u { get; set; }
        public int Id_d { get; set; }
        public bool Rate { get; set; }
        #endregion

        #region Constructors
        public Rating(MySqlDataReader reader)
        {
            Id_u = int.Parse(reader["id_u"].ToString());
            Id_d = int.Parse(reader["id_d"].ToString());
            Rate = bool.Parse(reader["rate"].ToString());
        }
        #endregion
    }
}
