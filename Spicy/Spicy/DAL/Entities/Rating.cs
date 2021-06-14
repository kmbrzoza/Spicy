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
        public uint? Id_u { get; set; }
        public uint? Id_d { get; set; }
        public Rate Rate;
        #endregion

        #region Constructors
        public Rating(MySqlDataReader reader)
        {
            Id_u = uint.Parse(reader["id_u"].ToString());
            Id_d = uint.Parse(reader["id_d"].ToString());
            Rate = (Rate)Enum.Parse(typeof(Rate), reader["rate"].ToString());
        }

        public Rating(Rate rate)
        {
            Id_u = null;
            Id_d = null;
            Rate = rate;
        }
        #endregion

        #region Methods
        public override bool Equals(object obj)
        {
            Rating rating = obj as Rating;
            if (rating is null) return false;
            if (Id_u != rating.Id_u) return false;
            if (Id_d != rating.Id_d) return false;

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
