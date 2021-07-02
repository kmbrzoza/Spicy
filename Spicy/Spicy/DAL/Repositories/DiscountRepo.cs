using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spicy.DAL.Repositories
{
    class DiscountRepo
    {
        #region QUERIES
        private const string GET_ALL_DISCOUNTS = @"SELECT d.name, d.description, d.discount_code, d.start_date, d.end_date, d.curr_price, d.prev_price, u.name, c.name, s.name
                                                    FROM discount d
                                                    INNER JOIN publishedby p ON p.id_discount=d.id_discount
                                                    INNER JOIN user u ON p.id_user=u.id_user
                                                    INNER JOIN isin i ON i.id_discount=d.id_discount
                                                    INNER JOIN category c ON c.id_category=i.id_category
                                                    INNER JOIN has h ON h.id_discount=d.id_discount
                                                    INNER JOIN shop s ON s.id_shop=h.id_shop;";
        private const string GET_DISCOUNTS_BY_CATEGORY = @"SELECT d.name, d.description, d.discount_code, d.start_date, d.end_date, d.curr_price, d.prev_price, u.name, c.name, s.name
                                                            FROM discount d
                                                            INNER JOIN publishedby p ON p.id_discount=d.id_discount
                                                            INNER JOIN user u ON p.id_user=u.id_user
                                                            INNER JOIN isin i ON i.id_discount=d.id_discount
                                                            INNER JOIN category c ON c.id_category=i.id_category
                                                            INNER JOIN has h ON h.id_discount=d.id_discount
                                                            INNER JOIN shop s ON s.id_shop=h.id_shop
                                                            WHERE c.name=";
        private const string ADD_DISCOUNT = @"INSERT INTO `discount`(`name`, `description`, `discount_code`, `start_date`, `end_date`, `curr_price`, `prev_price`) VALUES "; //nwm jeszcze od której strony sie za to zabrać póki co jest jak jest ^^
        #endregion
    }
}
