using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace Spicy.DAL.Repositories
{
    using Entities;
    static class DiscountRepo
    {
        #region QUERIES
        private const string GET_ALL_DISCOUNTS = @"SELECT * from discount";
        private const string ADD_DISCOUNT = @"INSERT INTO `discount`(`name`, `description`, `curr_price`, `prev_price`, `discount_code`, `start_date`, `end_date`, `link`, `image`, `id_category`, `id_user`, `id_shop`) VALUES ";
        private const string ImageParam = "@Image";
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

        public static bool AddDiscount(Discount discount)
        {
            bool status = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                string prevPrice = "null";
                string currPrice = "null";
                if (discount.CurrentPrice != null) currPrice = $"'{discount.CurrentPrice.ToString().Replace(",", ".")}'";
                if (discount.PreviousPrice != null) prevPrice = $"'{discount.PreviousPrice.ToString().Replace(",", ".")}'";

                MySqlCommand command = new MySqlCommand($"{ADD_DISCOUNT} ('{discount.Name}', '{discount.Description}', {currPrice}, {prevPrice}, " +
                $"'{discount.Code}', '{discount.Start_Date.Year}-{discount.Start_Date.Month}-{discount.Start_Date.Day}', " +
                $"'{discount.End_Date.Year}-{discount.End_Date.Month}-{discount.End_Date.Day}', " +
                $"'{discount.Link}', {ImageParam}, '{discount.Id_category}', '{discount.Id_user}', '{discount.Id_shop}')", connection);

                command.Parameters.Add(ImageParam, MySqlDbType.MediumBlob);
                command.Parameters[ImageParam].Value = discount.Image;

                connection.Open();
                command.ExecuteNonQuery();
                status = true;
                discount.Id = (uint)command.LastInsertedId;
                connection.Close();
            }
            return status;
        }

        public static bool UpdateDiscount(Discount discount, uint Id)
        {
            bool status = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                string prevPrice = "null";
                string currPrice = "null";
                if (discount.CurrentPrice != null) currPrice = $"'{discount.CurrentPrice.ToString().Replace(",", ".")}'";
                if (discount.PreviousPrice != null) prevPrice = $"'{discount.PreviousPrice.ToString().Replace(",", ".")}'";

                MySqlCommand command = new MySqlCommand($"UPDATE discount SET name = '{discount.Name}', description = '{discount.Description}', " +
                $"curr_price = {currPrice}, prev_price = {prevPrice}, link = '{discount.Link}', discount_code = '{discount.Code}', " +
                $"start_date = '{discount.Start_Date.Year}-{discount.Start_Date.Month}-{discount.Start_Date.Day}', " +
                $"end_date = '{discount.End_Date.Year}-{discount.End_Date.Month}-{discount.End_Date.Day}', image = {ImageParam}, id_category = '{discount.Id_category}', " +
                $"id_shop = '{discount.Id_shop}' WHERE id_discount = '{Id}'", connection);

                command.Parameters.Add(ImageParam, MySqlDbType.MediumBlob);
                command.Parameters[ImageParam].Value = discount.Image;

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

