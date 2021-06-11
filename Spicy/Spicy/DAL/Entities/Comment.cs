using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Spicy.DAL.Entities
{
    class Comment
    {
        #region Properties
        public int Id_u { get; set; }
        public int Id_d { get; set; }
        public string Comment { get; set; }
        #endregion

        #region Constructors
        public Comment(MySqlDataReader reader)
        {
            Id_u = int.Parse(reader["id_u"].ToString());
            Id_p = int.Parse(reader["id_d"].ToString());
            Comment = reader["comment"].ToString());
        }
        #endregion
    }
}
