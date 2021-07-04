using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spicy.Model
{
    class UserComment
    {
        public string Nickname { get; set; }
        public string CommentText { get; set; }
        public DateTime Date { get; set; }
        public UserComment(string nickname, string commentText, DateTime date)
        {
            this.Nickname = nickname;
            this.CommentText = commentText;
            this.Date = date;
        }
    }
}
