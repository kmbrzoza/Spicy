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
        public uint? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string Code { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }
        #endregion

        #region Constructors
        public Discount(MySqlDataReader reader)
        {
            Id = uint.Parse(reader["id_d"].ToString());
            Name = reader["name"].ToString();
            Description = reader["description"].ToString();
            Link = reader["link"].ToString();
            Code = reader["code"].ToString();
            Start_Date = DateTime.Parse(reader["start_Date"].ToString());
            End_Date = DateTime.Parse(reader["end_Date"].ToString());
        }

        public Discount(string name, string description, string link, string code, DateTime start, DateTime end)
        {
            Id = null;
            Name = name;
            Description = description;
            Link = link;
            Code = code;
            Start_Date = start;
            End_Date = end;
        }

        public Discount(string name, string description, string link, DateTime start, DateTime end)
        {
            Id = null;
            Name = name;
            Description = description;
            Link = link;
            Code = null;
            Start_Date = start;
            End_Date = end;
        }

        public Discount(Discount discount)
        {
            Id = discount.Id;
            Name = discount.Name;
            Description = discount.Description;
            Link = discount.Description;
            Start_Date = discount.Start_Date;
            End_Date = discount.End_Date;
        }
        #endregion

        #region Methods
        public override bool Equals(object obj)
        {
            Discount dsc = obj as Discount;
            if ( dsc is null ) return false;
            if (Name.ToLower() != dsc.Name.ToLower()) return false;

            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion
    }
}
