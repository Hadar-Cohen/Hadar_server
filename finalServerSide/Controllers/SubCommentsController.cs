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
    public class SubCommentsController : ApiController
    {
        // GET api/<controller>
        public int Post([FromBody] SubComment com)
        {
            com.PostSubComment();
            return com.SeriesId;
        }

        // GET api/<controller>/5
        public List<SubComment> Get(int seriesId, int commentId)
        {
            SubComment sc = new SubComment();
            return sc.Get(seriesId, commentId);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}