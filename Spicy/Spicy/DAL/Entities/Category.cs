using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Spicy.DAL.Entities
{
    class Category
    {
        #region Properties
        public uint? Id { get; set; }
        public string Name { get; set; }
        #endregion

        #region Constructors
        public Category(MySqlDataReader reader)
        {
            Id = uint.Parse(reader["id_category"].ToString());
            Name = reader["name"].ToString();
        }

        public Category(string name)
        {
            Id = null;
            Name = name;
        }
        #endregion
        //Raczej nie zakładamy dodawania kategorii bo jest ich raczej skończona ilość
    }
}
