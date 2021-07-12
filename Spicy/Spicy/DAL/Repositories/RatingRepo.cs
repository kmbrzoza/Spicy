using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Spicy.DAL.Repositories
{
    using Entities;
    static class RatingRepo
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
                command.ExecuteNonQuery();
                status = true;
                connection.Close();
            }
            return status;
        }

        public static bool UpdateRating(Rating rating, uint id_discount, uint id_user)
        {
            bool status = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"UPDATE rating SET rating = '{rating.Rate}' WHERE id_discount = '{id_discount}' and id_user = '{id_user}'", connection);
                connection.Open();
                var n = command.ExecuteNonQuery();
                if (n == 1) status = true;
                connection.Close();
            }
            return status;
        }
        #endregion
    }
}
