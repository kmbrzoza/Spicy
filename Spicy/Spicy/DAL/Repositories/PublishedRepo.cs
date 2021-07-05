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
        public List<Published> GetPublished()
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

        public bool AddHas(Has has)
        {
            bool status = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{ADD_HAS} {has.ToInsert()}", connection);
                connection.Open();
                var id = command.ExecuteNonQuery();
                status = true;
                has.Id_has = (uint)command.LastInsertedId;
                connection.Close();
            }
            return status;
        }
    }
}
