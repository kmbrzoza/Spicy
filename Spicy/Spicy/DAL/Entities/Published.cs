//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using MySql.Data.MySqlClient;

//namespace Spicy.DAL.Entities
//{
//    class Published
//    {
//        #region Properties
//        public uint Id_published { get; set; }
//        public uint Id_user { get; set; }
//        public uint Id_discount { get; set; }
//        #endregion

//        #region Constructors
//        public Published(uint id_user, uint id_discount)
//        {
//            Id_user = id_user;
//            Id_discount = id_discount;
//        }

//        public Published(uint id_published, uint id_user, uint id_discount)
//        {
//            Id_published = id_published;
//            Id_user = id_user;
//            Id_discount = id_discount;
//        }
//        #endregion

//        #region Methods
//        public Published(MySqlDataReader reader)
//        {
//            Id_published = uint.Parse(reader["id_published"].ToString());
//            Id_user = uint.Parse(reader["id_user"].ToString());
//            Id_discount = uint.Parse(reader["id_discount"].ToString());
//        }

//        public string ToInsert()
//        {
//            return $"('{Id_user}', '{Id_discount}')";
//        }
//        #endregion

//    }
//}
