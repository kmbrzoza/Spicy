using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Spicy.DAL.Entities
{
    class Has
    {
        #region Properties
        public uint? Id_s { get; set; }
        public uint? Id_d { get; set; }
        #endregion

        #region Constructors
        public Has(MySqlDataReader reader)
        {
            Id_s = uint.Parse(reader["id_s"].ToString());
            Id_d = uint.Parse(reader["id_d"].ToString());
        }
        #endregion
    }
}
