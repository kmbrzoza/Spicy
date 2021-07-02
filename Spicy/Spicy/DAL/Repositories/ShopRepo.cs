using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace Spicy.DAL.Repositories
{
    using Entities;
    class ShopRepo
    {
        #region QUERIES
        private const string GET_SHOPS = "SELECT * FROM shop";
        private const string GET_SHOP_NAMES = "SELECT name FROM shop";
        private const string ADD_SHOP = "INSERT INTO `shop`(`name`) VALUES ";
        #endregion

        #region METHODS
        public static bool AddShop(Shop shop)
        {
            bool status = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{ADD_SHOP} {shop.ToInsert()}", connection);
                connection.Open();
                var id = command.ExecuteNonQuery();
                status = true;
                shop.Id = (uint)command.LastInsertedId;
                connection.Close();
            }
            return status;
        }

        #endregion
    }
}
