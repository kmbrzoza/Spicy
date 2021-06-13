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
        public uint? Id_p { get; set; }
        public uint? Id_k { get; set; }
        #endregion

        #region Constructors
        public IsIn(MySqlDataReader reader)
        {
            Id_p = uint.Parse(reader["id_p"].ToString());
            Id_k = uint.Parse(reader["id_k"].ToString());
        }
        #endregion
    }
}
