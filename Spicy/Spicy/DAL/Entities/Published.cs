﻿using System;
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
        public uint? Id_published { get; set; }
        public uint Id_user { get; set; }
        public uint Id_discount { get; set; }
        #endregion

        #region Constructors
        public Published(MySqlDataReader reader)
        {
            Id_published = uint.Parse(reader["id_published"].ToString());
            Id_user = uint.Parse(reader["id_user"].ToString());
            Id_discount = uint.Parse(reader["id_discount"].ToString());
        }
        #endregion

    }
}
