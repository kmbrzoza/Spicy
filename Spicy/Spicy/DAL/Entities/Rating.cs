﻿using System;
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
        public uint? Id_u { get; set; }
        public uint? Id_d { get; set; }
        public int Rate { get; set; }
        #endregion

        #region Constructors
        public Rating(MySqlDataReader reader)
        {
            Id_u = uint.Parse(reader["id_u"].ToString());
            Id_d = uint.Parse(reader["id_d"].ToString());
            Rate = int.Parse(reader["rate"].ToString());
        }

        public Rating(int rate)
        {
            Id_u = null;
            Id_d = null;
            Rate = rate;
        }
        #endregion
    }
}
