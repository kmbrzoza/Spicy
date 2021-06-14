using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Spicy.DAL.Entities
{
    class IsIn
    {
        #region Properties
        public uint? Id_d { get; set; }
        public uint? Id_c { get; set; }
        #endregion

        #region Constructors
        public IsIn(MySqlDataReader reader)
        {
            Id_d = uint.Parse(reader["id_d"].ToString());
            Id_c = uint.Parse(reader["id_c"].ToString());
        }
        #endregion
    }
}
