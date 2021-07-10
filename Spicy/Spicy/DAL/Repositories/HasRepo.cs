using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Spicy.DAL.Repositories
{
    using Entities;

    class HasRepo
    {
        #region QUERIES
        private const string GET_HAS = @"SELECT * from has";
        private const string ADD_HAS = @"INSERT INTO `has`(`id_discount`, `id_shop`) VALUES ";
        #endregion

        #region METHODS
        public static List<Has> GetHas()
        {
            List<Has> has = new List<Has>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(GET_HAS, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                    has.Add(new Has(reader));
                connection.Close();
            }
            return has;
        }

        public static bool AddHas(Has has)
        {
            bool status = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{ADD_HAS} {has.ToInsert()}", connection);
                connection.Open();
                status = true;    
                connection.Close();
            }
            return status;
        }
        #endregion




    }
}
