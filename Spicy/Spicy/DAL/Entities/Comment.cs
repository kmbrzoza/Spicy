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
        public uint? Id_comment { get; set; }
        public uint Id_user { get; set; }
        public uint Id_discount { get; set; }
        public string CommentText { get; set; }
        public DateTime Date { get; set; }
        #endregion

        #region Constructors
        public Comment(MySqlDataReader reader)
        {
            Id_comment = uint.Parse(reader["id_comment"].ToString());
            Id_user = uint.Parse(reader["id_user"].ToString());
            Id_discount = uint.Parse(reader["id_discount"].ToString());
            CommentText = reader["comment"].ToString();
            Date = DateTime.Parse(reader["date"].ToString());
        }

        public Comment(string commenttext)
        {
            Id_comment = null;
            Id_user = Id_user;
            Id_discount = Id_discount;
            CommentText = commenttext;
        }
        #endregion
    }
}
