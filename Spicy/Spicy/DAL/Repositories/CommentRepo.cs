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
        //private const string ADD_COMMENT_TO_DISCOUNT = "";
        //#endregion

        #region METHODS
        //tu będzie sie działy inne rzeczy do tych komentarzy związków itd
        public List<Comment> GetCommentsForDiscount(Discount discount)
        {
            List<Comment> comments = new List<Comment>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"SELECT c.id_comment, c.comment_text, c.id_discount, c.id_user, c.date FROM comments c INNER JOIN user u ON u.id_user = c.id_user WHERE id_discount={discount.Id}", connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                    comments.Add(new Comment(reader));
                connection.Close();
            }
            return comments;
        }

        #endregion
    }
}
