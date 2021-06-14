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
        public uint? Id_has { get; set; }
        public uint Id_shop { get; set; }
        public uint Id_discount { get; set; }
        #endregion

        #region Constructors
        public Has(MySqlDataReader reader)
        {
            Id_has = uint.Parse(reader["id_has"].ToString());
            Id_shop = uint.Parse(reader["id_shop"].ToString());
            Id_discount = uint.Parse(reader["id_discount"].ToString());
        }
        #endregion
    }
}
