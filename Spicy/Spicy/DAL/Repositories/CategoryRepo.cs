using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spicy.DAL.Repositories
{
    class CategoryRepo
    {
        #region QUERIES
        private const string GET_ALL_CATEGORIES = "SELECT * FROM category";
        private const string GET_CATEGORIES_NAMES = "SELECT name FROM category";
        private const string ADD_CATEGORY = "INSERT INTO `category`(`name`) VALUES ";//nwm czy to sie przyda ale jes
        #endregion
    }
}
