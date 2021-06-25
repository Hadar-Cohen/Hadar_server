using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Ex2.Models;
using Ex2.Models.DAL;

namespace Ex2.Controllers
{
    public class UsersController : ApiController
    {
        public HttpResponseMessage Get(string email, string password)
        {
            User us = new User();
            User u= us.checkLogin(email, password);

            if (u !=null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, u);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "user not found");
            }
        }

        public User Get(int id)
        {
            User us = new User();
            return us.GetById(id);
        }

        public List<User> Get()
        {
            User us = new User();
            return us.Get();
        }
        // POST api/<controller>
        public int Post([FromBody] User user)
        {
            return user.Insert(); //return 1
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put(User user)
        {
            int num = user.UpdateUser();
            if (num == 1)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "User Updated");
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "user not found");
            }

        }

        // DELETE api/<controller>/5
        public int Delete(int id)
        {
            User us = new User();
            return us.Delete(id);
        }
    }
}