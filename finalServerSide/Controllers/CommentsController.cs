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
    public class CommentsController : ApiController
    {
        // GET api/<controller>
        public List<Comment> Get(int seriesId, int connectedUserId)
        {
            Comment c = new Comment();
            return c.Get(seriesId, connectedUserId);
        }
        //public int Get()
        //{
        //    Comment c = new Comment();
        //    return c.Get(); 
        //}

        // POST api/<controller>
        public int Post([FromBody] Comment com)
        {
            com.PostComment();
            return com.SeriesId;
                
        }

        // PUT api/<controller>/5
        public int Put(int commentId, int likes, int dislikes)
        {
            Comment c = new Comment();
            c.UpdateLikes(commentId, likes, dislikes);
            return commentId;
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}