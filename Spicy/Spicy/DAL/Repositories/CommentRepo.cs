using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace Spicy.DAL.Repositories
{
    using Entities;
    static class CommentRepo
    {
        #region QUERIES
        private const string GET_COMMENTS = "SELECT c.comment_text, c.id_discount, c.id_user, c.date FROM comments c";
        private const string ADD_COMMENT_FOR_DISCOUNT = "INSERT INTO `comments`(`comment_text`, `id_user`, `id_discount`, `date`) VALUES ";
        #endregion

        #region METHODS
        public static List<Comment> GetComments()
        {
            List<Comment> comments = new List<Comment>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(GET_COMMENTS, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                    comments.Add(new Comment(reader));
                connection.Close();
            }
            return comments;
        }

        public static bool AddComment(Comment comment)
        {
            bool status = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{ADD_COMMENT_FOR_DISCOUNT} {comment.ToInsert()}", connection);
                connection.Open();
                command.ExecuteNonQuery();
                status = true;
                connection.Close();
            }
            return status;
        }

        #endregion
    }
}
