using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Spicy.DAL.Entities
{
    class Comment
    {
        #region Properties
        public uint Id_user { get; set; }
        public uint Id_discount { get; set; }
        public string CommentText { get; set; }
        public DateTime Date { get; set; }
        #endregion

        #region Constructors
        //DATA POWINNA BYĆ PODANA W ODPOWIEDNIM DLA MYSQL FORMACIE
        public Comment(MySqlDataReader reader)
        {
            Id_user = uint.Parse(reader["id_user"].ToString());
            Id_discount = uint.Parse(reader["id_discount"].ToString());
            CommentText = reader["comment_text"].ToString();
            Date = DateTime.Parse(reader["date"].ToString());
        }

        public Comment(uint id_user, uint id_discount, string commenttext, DateTime date)
        {
            Id_user = id_user;
            Id_discount = id_discount;
            CommentText = commenttext;
            Date = date;
        }

        public Comment(string commenttext)
        {
            Id_user = Id_user;
            Id_discount = Id_discount;
            CommentText = commenttext;
            Date = Date;
        }
        #endregion

        #region Methods
        public string ToInsert()
        {
            return $"('{CommentText}', '{Id_user}', '{Id_discount}', '{Date.Year}-{Date.Month}-{Date.Day}')";
        }
        #endregion
    }
}
