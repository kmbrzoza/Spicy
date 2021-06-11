using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Spicy.DAL.Entities
{
	class Discount
	{
        #region Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }
        #endregion

        #region Constructors
        public Discount(MySqlDataReader reader)
        {
            Id = int.Parse(reader["id_d"].ToString());
            Name = reader["name"].ToString();
            Description = reader["description"].ToString();
            Link = reader["link"].ToString();
            Start_Date = DateTime.Parse(reader["start_Date"].ToString());
            Start_Date = DateTime.Parse(reader["end_Date"].ToString());
        }
        #endregion
    }
}
