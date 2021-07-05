using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Spicy.DAL.Repositories
{
    using Entities;

    class IsInRepo
    {
        #region QUERIES
        private const string GET_ISIN = @"SELECT * from isin";
        private const string ADD_ISIN = @"INSERT INTO `isin`(`id_discount`, `id_category`) VALUES ";
        #endregion

        #region METHODS
        public List<IsIn> GetHas()
        {
            List<IsIn> isin = new List<IsIn>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(GET_ISIN, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                    isin.Add(new IsIn(reader));
                connection.Close();
            }
            return isin;
        }

        public bool AddHas(IsIn isin)
        {
            bool status = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{ADD_ISIN} {isin.ToInsert()}", connection);
                connection.Open();
                var id = command.ExecuteNonQuery();
                status = true;
                isin.Id_isin = (uint)command.LastInsertedId;
                connection.Close();
            }
            return status;
        }
        #endregion
    }
}
