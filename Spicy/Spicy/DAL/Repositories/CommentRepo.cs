using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
//Część zapytań może być pusta bo jeszcze ich sb nie opracowałam do końca ;__;
namespace Spicy.DAL.Repositories
{
    using Entities;
    class CommentRepo
    {
        //#region QUERIES
        //private const string GET_COMMENTS_FOR_DISCOUNT = "";
        private const string ADD_COMMENT_FOR_DISCOUNT = "INSERT INTO `comments`(`comment_text`, `id_user`, 'id_discount', 'date') VALUES ";
        //#endregion

        #region METHODS
        //tu będzie sie działy inne rzeczy do tych komentarzy związków itd
        public List<Comment> GetComments(Discount discount)
        {
            List<Comment> comments = new List<Comment>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"SELECT c.id_comment, c.comment_text, c.id_discount, c.id_user, c.date FROM comments c", connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                    comments.Add(new Comment(reader));
                connection.Close();
            }
            return comments;
        }

        public bool AddComment(Comment comment)
        {
            bool status = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{ADD_COMMENT_FOR_DISCOUNT} {comment.ToInsert()}", connection);
                connection.Open();
                var id = command.ExecuteNonQuery();
                status = true;
                comment.Id_comment = (uint)command.LastInsertedId;
                connection.Close();
            }
            return status;
        }

        #endregion
    }
}
