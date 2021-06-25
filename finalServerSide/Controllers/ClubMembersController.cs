using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using finalServerSide.Models;

namespace finalServerSide.Controllers
{
    public class ClubMembersController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public ClubMember Get(int seriesId, int userId)
        {
            ClubMember cm = new ClubMember();
            return cm.Get(seriesId, userId);

            
            //if (cm.UserId != 0)
            //{
            //    return Request.CreateResponse(HttpStatusCode.OK, cm);
            //}
            //else
            //{
            //    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "error");
            //}
        }

        // POST api/<controller>
        public int Post(int seriesId, int userId)
        {
            ClubMember cm = new ClubMember();
            return cm.Insert(seriesId, userId);

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