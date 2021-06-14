using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Spicy.DAL.Entities
{
    class Published
    {
        #region Properties
        public uint? Id_p { get; set; }
        public uint? Id_u { get; set; }
        public uint? Id_d { get; set; }
        #endregion

        #region Constructors
        public Published(MySqlDataReader reader)
        {
            Id_p = uint.Parse(reader["id_p"].ToString());
            Id_u = uint.Parse(reader["id_u"].ToString());
            Id_d = uint.Parse(reader["id_d"].ToString());
        }
        #endregion

    }
}
