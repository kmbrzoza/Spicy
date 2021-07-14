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
        private const string ADD_DISCOUNT_PRICESNULL = @"INSERT INTO `discount`(`name`, `description`, `discount_code`, `start_date`, `end_date`, `link`, `image`, `id_category`, `id_user`, `id_shop`) VALUES ";

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
            MySqlCommand command = null;
            using (var connection = DBConnection.Instance.Connection)
            {
                if (discount.PreviousPrice.HasValue == false & discount.CurrentPrice.HasValue == false)
                {
                    string add_discount = @"INSERT INTO `discount`(`name`, `description`, `discount_code`, `start_date`, `end_date`, `link`, `image`, `id_category`, `id_user`, `id_shop`) VALUES
                                                              (@name, @description, @discount_code, @start_date, @end_date, @link, @image, @id_category, @id_user, @id_shop)";
                    command = new MySqlCommand(add_discount, connection);
                    command.Parameters.Add("@name", MySqlDbType.VarChar, 50);
                    command.Parameters.Add("@description", MySqlDbType.Text);
                    command.Parameters.Add("@discount_code", MySqlDbType.VarChar, 30);
                    command.Parameters.Add("@start_date", MySqlDbType.DateTime);
                    command.Parameters.Add("@end_date", MySqlDbType.DateTime);
                    command.Parameters.Add("@link", MySqlDbType.VarChar, 2048);
                    command.Parameters.Add("@image", MySqlDbType.Blob);
                    command.Parameters.Add("@id_category", MySqlDbType.UInt32);
                    command.Parameters.Add("@id_user", MySqlDbType.UInt32);
                    command.Parameters.Add("@id_shop", MySqlDbType.UInt32);

                    command.Parameters["@name"].Value = discount.Name;
                    command.Parameters["@description"].Value = discount.Description;
                    command.Parameters["@discount_code"].Value = discount.Code;
                    command.Parameters["@start_date"].Value = discount.Start_Date;
                    command.Parameters["@end_date"].Value = discount.End_Date;
                    command.Parameters["@link"].Value = discount.Link;
                    command.Parameters["@image"].Value = discount.Image;
                    command.Parameters["@id_category"].Value = discount.Id_category;
                    command.Parameters["@id_user"].Value = discount.Id_user;
                    command.Parameters["@id_shop"].Value = discount.Id_shop;
                }
                else
                {
                    string add_discount = @"INSERT INTO `discount`(`name`, `description`, `curr_price`, `prev_price`, `discount_code`, `start_date`, `end_date`, `link`, `image`, `id_category`, `id_user`, `id_shop`) VALUES
                                                              (@name, @description, @curr_price, @prev_price, @discount_code, @start_date, @end_date, @link, @image, @id_category, @id_user, @id_shop)";
                    command = new MySqlCommand(add_discount, connection);
                    command.Parameters.Add("@name", MySqlDbType.VarChar, 50);
                    command.Parameters.Add("@description", MySqlDbType.Text);
                    command.Parameters.Add("@curr_price", MySqlDbType.Float);
                    command.Parameters.Add("@prev_price", MySqlDbType.Float);
                    command.Parameters.Add("@discount_code", MySqlDbType.VarChar, 30);
                    command.Parameters.Add("@start_date", MySqlDbType.DateTime);
                    command.Parameters.Add("@end_date", MySqlDbType.DateTime);
                    command.Parameters.Add("@link", MySqlDbType.VarChar, 2048);
                    command.Parameters.Add("@image", MySqlDbType.Blob);
                    command.Parameters.Add("@id_category", MySqlDbType.UInt32);
                    command.Parameters.Add("@id_user", MySqlDbType.UInt32);
                    command.Parameters.Add("@id_shop", MySqlDbType.UInt32);

                    command.Parameters["@name"].Value = discount.Name;
                    command.Parameters["@description"].Value = discount.Description;
                    command.Parameters["@curr_price"].Value = discount.CurrentPrice;
                    command.Parameters["@prev_price"].Value = discount.PreviousPrice;
                    command.Parameters["@discount_code"].Value = discount.Code;
                    command.Parameters["@start_date"].Value = discount.Start_Date;
                    command.Parameters["@end_date"].Value = discount.End_Date;
                    command.Parameters["@link"].Value = discount.Link;
                    command.Parameters["@image"].Value = discount.Image;
                    command.Parameters["@id_category"].Value = discount.Id_category;
                    command.Parameters["@id_user"].Value = discount.Id_user;
                    command.Parameters["@id_shop"].Value = discount.Id_shop;
                }

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
                MySqlCommand command = null;
                using (var connection = DBConnection.Instance.Connection)
                {
                    if (discount.PreviousPrice.HasValue == false && discount.CurrentPrice.HasValue == false)
                    {
                        string update_discount = $"SET foreign_key_checks = 0; UPDATE discount SET name = @name , description = @description, link = @link, discount_code = @discount_code, start_date = @start_date, end_date = @end_date, image = @image, id_category = @id_category, id_shop = @id_shop WHERE id_discount = @id_discount; SET foreign_key_checks = 1";
                        command = new MySqlCommand(update_discount, connection);
                        command.Parameters.Add("@name", MySqlDbType.VarChar, 50);
                        command.Parameters.Add("@description", MySqlDbType.Text);
                        command.Parameters.Add("@discount_code", MySqlDbType.VarChar, 30);
                        command.Parameters.Add("@start_date", MySqlDbType.DateTime);
                        command.Parameters.Add("@end_date", MySqlDbType.DateTime);
                        command.Parameters.Add("@link", MySqlDbType.VarChar, 2048);
                        command.Parameters.Add("@image", MySqlDbType.Blob);
                        command.Parameters.Add("@id_category", MySqlDbType.UInt32);
                        command.Parameters.Add("@id_discount", MySqlDbType.UInt32);
                        command.Parameters.Add("@id_shop", MySqlDbType.UInt32);

                        command.Parameters["@name"].Value = discount.Name;
                        command.Parameters["@description"].Value = discount.Description;
                        command.Parameters["@discount_code"].Value = discount.Code;
                        command.Parameters["@start_date"].Value = discount.Start_Date;
                        command.Parameters["@end_date"].Value = discount.End_Date;
                        command.Parameters["@link"].Value = discount.Link;
                        command.Parameters["@image"].Value = discount.Image;
                        command.Parameters["@id_shop"].Value = discount.Id_shop;
                        command.Parameters["@id_discount"].Value = Id;
                        command.Parameters["@id_category"].Value = discount.Id_category;
                    }
                    else
                    {
                        string update_discount = $"SET foreign_key_checks = 0; UPDATE discount SET name = @name , description = @description, curr_price = @curr_price, prev_price = @prev_price, link = @link, discount_code = @discount_code, start_date = @start_date, end_date = @end_date, image = @image, id_category = @id_category, id_shop = @id_shop WHERE id_discount = @id_discount; SET foreign_key_checks = 1";
                        command = new MySqlCommand(update_discount, connection);
                        command.Parameters.Add("@name", MySqlDbType.VarChar, 50);
                        command.Parameters.Add("@description", MySqlDbType.Text);
                        command.Parameters.Add("@curr_price", MySqlDbType.Float);
                        command.Parameters.Add("@prev_price", MySqlDbType.Float);
                        command.Parameters.Add("@discount_code", MySqlDbType.VarChar, 30);
                        command.Parameters.Add("@start_date", MySqlDbType.DateTime);
                        command.Parameters.Add("@end_date", MySqlDbType.DateTime);
                        command.Parameters.Add("@link", MySqlDbType.VarChar, 2048);
                        command.Parameters.Add("@image", MySqlDbType.Blob);
                        command.Parameters.Add("@id_category", MySqlDbType.UInt32);
                        command.Parameters.Add("@id_discount", MySqlDbType.UInt32);
                        command.Parameters.Add("@id_shop", MySqlDbType.UInt32);

                        command.Parameters["@name"].Value = discount.Name;
                        command.Parameters["@description"].Value = discount.Description;
                        command.Parameters["@curr_price"].Value = discount.CurrentPrice;
                        command.Parameters["@prev_price"].Value = discount.PreviousPrice;
                        command.Parameters["@discount_code"].Value = discount.Code;
                        command.Parameters["@start_date"].Value = discount.Start_Date;
                        command.Parameters["@end_date"].Value = discount.End_Date;
                        command.Parameters["@link"].Value = discount.Link;
                        command.Parameters["@image"].Value = discount.Image;
                        command.Parameters["@id_shop"].Value = discount.Id_shop;
                        command.Parameters["@id_discount"].Value = Id;
                        command.Parameters["@id_category"].Value = discount.Id_category;
                    }
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

