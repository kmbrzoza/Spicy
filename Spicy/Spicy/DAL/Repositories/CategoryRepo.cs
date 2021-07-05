using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Spicy.DAL.Repositories
{
    using Entities;
    class CategoryRepo
    {
        #region QUERIES
        private const string GET_ALL_CATEGORIES = "SELECT * FROM category";
        #endregion

        #region METHODS
        public static List<Category> GetCategories()
        {
            List <Category> categories= new List<Category>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(GET_ALL_CATEGORIES, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                    categories.Add(new Category(reader));
                connection.Close();
            }
            return categories;
        }
        #endregion
    }
}
