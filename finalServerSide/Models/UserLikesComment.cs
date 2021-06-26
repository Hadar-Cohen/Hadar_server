using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using finalServerSide.Models.DAL;

namespace finalServerSide.Models
{
    public class UserLikesComment
    {
        int commentId;
        int userId;
        int seriesId;
        bool like;
        bool dislike;
        public UserLikesComment()
        {

        }
        public UserLikesComment(int commentId, int userId, int seriesId, bool like, bool dislike)
        {
            this.commentId = commentId;
            this.userId = userId;
            this.seriesId = seriesId;
            this.like = like;
            this.dislike = dislike;
        }

        public int CommentId { get => commentId; set => commentId = value; }
        public int UserId { get => userId; set => userId = value; }
        public int SeriesId { get => seriesId; set => seriesId = value; }
        public bool Like { get => like; set => like = value; }
        public bool Dislike { get => dislike; set => dislike = value; }

        public int Insert()
        {
            UserLikeCommDBServices ulc = new UserLikeCommDBServices();
            return ulc.Insert(this); 
        }

        public int Update()
        {
            UserLikeCommDBServices ulc = new UserLikeCommDBServices();
            return ulc.Update(this);
        }

        public List<UserLikesComment> GetList()
        {
            UserLikeCommDBServices ulc = new UserLikeCommDBServices();
            return ulc.Get();
        }
    }
}