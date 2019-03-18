using BlogSite.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BlogSite.Controllers
{
    public class UserController : ApiController
    {
        BlogSiteEntities db;

        public UserController()
        {
            db = new BlogSiteEntities();
        }
        
        [HttpPost]
        public IHttpActionResult Add(User user)
        {
            if (string.IsNullOrWhiteSpace(user.UserName))
            {
                return Json<string>("User name cannot be empty!");
            }
            db.User.Add(user);
            db.SaveChanges();
            return Json("User added");
        }
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                User deletedUser = db.User.Find(id);
                db.User.Remove(deletedUser);
                db.SaveChanges();
                return Json("User deleted");            }
            catch (Exception ex)
            {
                return Json(ex.Message);
                
            }
        }
        [HttpPut]
        public IHttpActionResult Update(User user)
        {
            if (string.IsNullOrWhiteSpace(user.UserName))
            {
                return Json<string>("User name cannot be empty");
            }
            User updatedUser = db.User.Find(user.UserName);
            updatedUser.UserName = user.UserName;
            updatedUser.Password = user.UserName;
            updatedUser.Name = user.Name;
            updatedUser.Surname = user.UserName;
            db.SaveChanges();
            return Json("user updated");
        }
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            try
            {
                User getUser = db.User.Find(id);
                if(getUser==null)
                {
                    return Json("User not found!");
                }
                UserDto u = new UserDto();
                u.Name = getUser.Name;
                u.SurName = getUser.Surname;
                u.UserName = getUser.UserName;
                u.Email = getUser.Email;
                u.Password = getUser.Password;
                return Json(u);
            }
            catch (Exception ex)
            {

                return Json(ex.Message);
            }
        }
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            try
            {
                List<UserDto> users = new List<UserDto>();
                foreach (User item in db.User.ToList())
                {
                    UserDto u = new UserDto();
                    u.Name = item.Name;
                    u.SurName =  item.Surname;
                    u.UserName = item.UserName;
                    u.Email =    item.Email;
                    u.Password = item.Password;
                    users.Add(u);
                }
                return Json(users);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
                
            }
        }
        [HttpGet]
        public IHttpActionResult GetUserByLogin(string mail,string password)
        {
            User u = db.User.Where(x => x.Email == mail && x.Password == password).SingleOrDefault();
            if(u==null)
            {
                return Json("Wrong email or password!");

            }
            UserDto uDto = new UserDto();
            uDto.Name = u.Name;
            uDto.SurName = u.Surname;
            uDto.UserName = u.UserName;
            uDto.Email = u.Email;
            uDto.Password = u.Password;
            uDto.UserRole = u.UserRole.Name;
            return Json(uDto);
        }

    }
}
