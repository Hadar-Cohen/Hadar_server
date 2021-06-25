using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using finalServerSide.Models.DAL;

namespace finalServerSide.Models.DAL
{
    public class Comment
    {
        int commentId;
        string currDate;
        int userId;
        string userName;
        int seriesId;
        string content;
        public Comment() { }
        public Comment(int commentId, string currDate, int userId, string userName, int seriesId, string content)
        {
            this.commentId = commentId;
            this.currDate = currDate;
            this.userId = userId;
            this.seriesId = seriesId;
            this.content = content;
            this.userName = userName;
        }

        public int CommentId { get => commentId; set => commentId = value; }
        public string CurrDate { get => currDate; set => currDate = value; }
        public int UserId { get => userId; set => userId = value; }
        public string UserName { get => userName; set => userName = value; }
        public int SeriesId { get => seriesId; set => seriesId = value; }
        public string Content { get => content; set => content = value; }
        

        public int PostComment()
        {
            CommentDBServices db = new CommentDBServices();
            return db.Insert(this); //return 1/-1;
        }

        public List<Comment> Get(int seriesId)
        {
            CommentDBServices db = new CommentDBServices();
            return db.GetComments(seriesId);
        }
        public int Get()
        {
            CommentDBServices db = new CommentDBServices();
            return db.GetMostActivUser();
        }
    }
}