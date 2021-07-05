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
        public float? CurrentPrice { get; set;}
        public float? PreviousPrice { get; set; }
        public string Link { get; set; }
        public string Code { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }
        #endregion

        #region Constructors
        public Discount(MySqlDataReader reader)
        {
            Id = uint.Parse(reader["id_discount"].ToString());
            Name = reader["name"].ToString();
            Description = reader["description"].ToString();
            CurrentPrice = float.Parse(reader["curr_price"].ToString());
            PreviousPrice = float.Parse(reader["prev_price"].ToString());
            Link = reader["link"].ToString();
            Code = reader["discount_code"].ToString();
            Start_Date = DateTime.Parse(reader["start_date"].ToString());
            End_Date = DateTime.Parse(reader["end_date"].ToString());
        }

        //not nulls

        public Discount(string name, string description, float? currentPrice, float? previousPrice, string link, string code, DateTime start, DateTime end)
        {
            Id = null;
            Name = name;
            Description = description;
            CurrentPrice = currentPrice;
            PreviousPrice = previousPrice;
            Link = link;
            Code = code;
            Start_Date = start;
            End_Date = end;
        }

        //only code null

        public Discount(string name, string description, float currentPrice, float previousPrice, string link, DateTime start, DateTime end)
        {
            Id = null;
            Name = name;
            Description = description;
            CurrentPrice = currentPrice;
            PreviousPrice = previousPrice;
            Link = link;
            Code = null;
            Start_Date = start;
            End_Date = end;
        }
        //nullables are null
        //public Discount(string name, string description, string link, DateTime start, DateTime end)
        //{
        //    Id = null;
        //    Name = name;
        //    Description = description;
        //    CurrentPrice = null;
        //    PreviousPrice = null;
        //    Link = link;
        //    Code = null;
        //    Start_Date = start;
        //    End_Date = end;
        //}

        //prices are null

        //public Discount(string name, string description, string link, string code, DateTime start, DateTime end)
        //{
        //    Id = null;
        //    Name = name;
        //    Description = description;
        //    CurrentPrice = null;
        //    PreviousPrice = null;
        //    Link = link;
        //    Code = code;
        //    Start_Date = start;
        //    End_Date = end;
        //}

        public Discount(Discount discount)
        {
            Id = discount.Id;
            Name = discount.Name;
            Description = discount.Description;
            CurrentPrice = discount.CurrentPrice;
            PreviousPrice = discount.PreviousPrice;
            Link = discount.Description;
            Code = discount.Code;
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

        public string ToInsert()
        {
            return $"('{Name}', '{Description}', '{CurrentPrice}', '{PreviousPrice}', '{Code}', '{Start_Date}', '{End_Date}')"; //'{Link}' bez tego narazie bo nie ma tego w bazie ://
        }
        #endregion
    }
}
