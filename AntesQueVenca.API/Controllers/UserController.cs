using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AntesQueVenca.Data.Context;
using AntesQueVenca.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AntesQueVenca.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        AntesQueVencaContext _aqvContext = new AntesQueVencaContext();

         [HttpPost]
         [AllowAnonymous]
         public void PostUser(User user)
        {
            _aqvContext.User.Add(user);
            _aqvContext.SaveChanges();
        }

        [HttpPut]
        [AllowAnonymous] 
        public void UpdateUser(User user)
        {
            _aqvContext.User.Update(user);
            _aqvContext.SaveChanges();
        }

        [HttpGet]
        [AllowAnonymous]
        public User GetUser(int id)
        {
            var selectedUser = _aqvContext.User.Find(id);

            return selectedUser;
        }

        [HttpGet]
        [AllowAnonymous]
        public List<User> GetAllUser()
        {
            var userList = _aqvContext.User.ToList();

            return userList;
        }

        [HttpDelete]
        [AllowAnonymous]
        public void DeleteUser(int id)
        {
            var selectedUser = _aqvContext.User.Find(id);
            _aqvContext.User.Remove(selectedUser);
            _aqvContext.SaveChanges();
        }
    }
}
