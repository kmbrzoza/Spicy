using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Spicy.DAL.Entities
{
    class Shop
    {
        #region Properties
        public uint Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        #endregion

        #region Constructors
        public Shop(MySqlDataReader reader)
        {
            Id = uint.Parse(reader["id_shop"].ToString());
            Name = reader["name"].ToString();
            Description = reader["description"].ToString();
        }

        public Shop(string name, string description)
        {
            Name = name;
            Description = description;
        }
        #endregion

        #region Methods
        public override bool Equals(object obj)
        {
            Shop shop = obj as Shop;
            if (shop is null) return false;
            if (Name.ToLower() != shop.Name.ToLower()) return false;

            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public string ToInsert()
        {
            return $"('{Name}', '{Description}')";
        }

        #endregion
        //Sklepy możnaby dodawać
    }
}
