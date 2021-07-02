using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spicy.DAL.Repositories
{
    class ShopRepo
    {
        #region QUERIES
        private const string GET_SHOPS = "SELECT * FROM shop";
        private const string GET_SHOP_NAMES = "SELECT name FROM shop";
        private const string ADD_SHOP = "INSERT INTO `shop`(`name`) VALUES ";
        #endregion
    }
}
