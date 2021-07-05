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
        public IsIn(uint id_discount, uint id_category)
        {
            Id_isin = null;
            Id_discount = id_discount;
            Id_category = id_category;
        }

        public IsIn(uint id_isin, uint id_discount, uint id_category)
        {
            Id_isin = id_isin;
            Id_discount = id_discount;
            Id_category = id_category;
        }
        #endregion

        #region Methods
        public IsIn(MySqlDataReader reader)
        {
            Id_isin = uint.Parse(reader["id_in"].ToString());
            Id_discount = uint.Parse(reader["id_discount"].ToString());
            Id_category = uint.Parse(reader["id_category"].ToString());
        }

        public string ToInsert()
        {
            return $"('{Id_discount}', '{Id_category}')";
        }
        #endregion
    }
}
