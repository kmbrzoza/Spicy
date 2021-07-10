using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace Spicy.DAL.Repositories
{
    using Entities;
    class DiscountRepo
    {
        #region QUERIES
        private const string GET_ALL_DISCOUNTS = @"SELECT * from discount";
        private const string ADD_DISCOUNT = @"INSERT INTO `discount`(`name`, `description`, `curr_price`, `prev_price`, `discount_code`, `start_date`, `end_date`, `link`, `image`, `id_category`, `id_user`) VALUES ";
        #endregion

        #region METHODS
        public static List<Discount> GetDiscounts()
        {
            List<Discount> discounts = new List<Discount>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(GET_ALL_DISCOUNTS, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                    discounts.Add(new Discount(reader));
                connection.Close();
            }
            return discounts;
        }

        public bool AddDiscount(ref Discount discount)
        {
            bool status = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{ADD_DISCOUNT} {discount.ToInsert()}", connection);
                connection.Open();
                status = true;
                discount.Id = (uint)command.LastInsertedId;
                connection.Close();
            }
            return status;
        }

        #endregion
    }
}
