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
        public int Id_u { get; set; }
        public int Id_d { get; set; }
        public string CommentText { get; set; }
        #endregion

        #region Constructors
        public Comment(MySqlDataReader reader)
        {
            Id_u = int.Parse(reader["id_u"].ToString());
            Id_d = int.Parse(reader["id_d"].ToString());
            CommentText = reader["comment"].ToString();
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
