using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using finalServerSide.Models;
using finalServerSide.Models.DAL;


namespace finalServerSide.Controllers
{
    public class UserLikesCommController : ApiController
    {
        // GET api/<controller>/5
        public List<UserLikesComment> Get()
        {
            UserLikesComment c = new UserLikesComment();
            return c.GetList();
        }

        // POST api/<controller>
        public void Post(UserLikesComment ulc)
        {
            ulc.Insert();
        }

        // PUT api/<controller>/5
        public void Put(int commentId, int userId, int seriesId, bool like, bool dislike)//, bool isLikes
        {
            UserLikesComment ulc = new UserLikesComment(commentId, userId, seriesId, like, dislike);
            ulc.Update();
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}