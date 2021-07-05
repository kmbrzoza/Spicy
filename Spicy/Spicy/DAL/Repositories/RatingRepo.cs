using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Spicy.DAL.Repositories
{
    using Entities;
    class RatingRepo
    {
        #region QUERIES
        private const string GET_RATING = @"SELECT * from rating";
        private const string ADD_RATING = @"INSERT INTO `rating`(`id_user`, `id_discount`, `rate`) VALUES ";
        #endregion

        #region METHODS 
        public static List<Rating> GetRating()
        {
            List<Rating> rating = new List<Rating>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(GET_RATING, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                    rating.Add(new Rating(reader));
                connection.Close();
            }
            return rating;
        }

        public static bool AddRating(Rating rating)
        {
            bool status = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{ADD_RATING} {rating.ToInsert()}", connection);
                connection.Open();
                var id = command.ExecuteNonQuery();
                status = true;
                rating.Id_rate = (uint)command.LastInsertedId;
                connection.Close();
            }
            return status;
        }
        #endregion
    }
}
