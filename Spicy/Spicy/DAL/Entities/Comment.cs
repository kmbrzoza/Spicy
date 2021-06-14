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
        public uint? Id_u { get; set; }
        public uint? Id_d { get; set; }
        public string CommentText { get; set; }
        public DateTime Date { get; set; }
        #endregion

        #region Constructors
        public Comment(MySqlDataReader reader)
        {
            Id_u = uint.Parse(reader["id_u"].ToString());
            Id_d = uint.Parse(reader["id_d"].ToString());
            CommentText = reader["comment"].ToString();
            Date = DateTime.Parse(reader["date"].ToString());
        }

        public Comment(string commenttext)
        {
            Id_u = null;
            Id_d = null;
            CommentText = commenttext;
        }
        #endregion
    }
}
