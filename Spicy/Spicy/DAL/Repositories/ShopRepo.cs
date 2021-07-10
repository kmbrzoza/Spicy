using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace Spicy.DAL.Repositories
{
    using Entities;
    static class ShopRepo
    {
        #region QUERIES
        private const string GET_SHOPS = "SELECT * FROM shop";
        private const string ADD_SHOP = "INSERT INTO `shop`(`name`) VALUES ";
        #endregion

        #region METHODS
        public static List<Shop> GetShops()
        {
            List<Shop> shops = new List<Shop>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(GET_SHOPS, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                    shops.Add(new Shop(reader));
                connection.Close();
            }
            return shops;
        }

        public static bool AddShop(Shop shop)
        {
            bool status = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{ADD_SHOP} {shop.ToInsert()}", connection);
                connection.Open();
                status = true;
                shop.Id = (uint)command.LastInsertedId;
                connection.Close();
            }
            return status;
        }

        #endregion
    }
}
