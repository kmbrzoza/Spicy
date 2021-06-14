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
        public uint? Id_rate { get; set; }
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

        public Rating(Rate rate)
        {
            Id_rate = null;
            Id_user = Id_user;
            Id_discount = Id_discount;
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
        #endregion
    }
    enum Rate { negative, positive }


}
