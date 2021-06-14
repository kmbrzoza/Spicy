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
        public uint? Id_isin { get; set; }
        public uint Id_discount { get; set; }
        public uint Id_category { get; set; }
        #endregion

        #region Constructors
        public IsIn(MySqlDataReader reader)
        {
            Id_isin = uint.Parse(reader["id_in"].ToString());
            Id_discount = uint.Parse(reader["id_discount"].ToString());
            Id_category = uint.Parse(reader["id_category"].ToString());
        }
        #endregion
    }
}
