using System;
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
        public uint Id_rate { get; set; }
        public uint Id_user { get; set; }
        public uint Id_discount { get; set; }
        public Rate Rate;
        #endregion

        #region Constructors
        public Rating(MySqlDataReader reader)
        {
            Id_rate = uint.Parse(reader["id_rate"].ToString());
            Id_user = uint.Parse(reader["id_user"].ToString());
            Id_discount = uint.Parse(reader["id_discount"].ToString());
            Rate = (Rate)Enum.Parse(typeof(Rate), reader["rate"].ToString());
        }

        public Rating(Rate rate, uint id_user, uint id_discount)
        {
            Id_user = id_user;
            Id_discount = id_discount;
            Rate = rate;
        }

        public Rating(uint id_rate, Rate rate, uint id_user, uint id_discount)
        {
            Id_rate = id_rate;
            Id_user = id_user;
            Id_discount = id_discount;
            Rate = rate;
        }
        #endregion

        #region Methods
        public override bool Equals(object obj)
        {
            Rating rating = obj as Rating;
            if (rating is null) return false;
            if (Id_user != rating.Id_user) return false;
            if (Id_discount != rating.Id_discount) return false;

            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public string ToInsert()
        {
            return $"('{Id_user}', '{Id_discount}', '{Rate}')";
        }
        #endregion
    }
    enum Rate { negative, positive }


}
