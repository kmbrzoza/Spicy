using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Spicy.DAL.Repositories
{
    using Entities;
    class PublishedRepo
    {
        #region QUERIES
        private const string GET_PUBLISHED = @"SELECT * from publishedby";
        private const string ADD_PUBLISHED = @"INSERT INTO `publishedby`(`id_user`, `id_discount`) VALUES ";
        #endregion

        #region METHODS
        public static List<Published> GetPublished()
        {
            List<Published> published = new List<Published>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(GET_PUBLISHED, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                    published.Add(new Published(reader));
                connection.Close();
            }
            return published;
        }

        public static bool AddPublished(Published published)
        {
            bool status = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{ADD_PUBLISHED} {published.ToInsert()}", connection);
                connection.Open();
                var id = command.ExecuteNonQuery();
                status = true;
                published.Id_published = (uint)command.LastInsertedId;
                connection.Close();
            }
            return status;
        }
        #endregion
    }
}
