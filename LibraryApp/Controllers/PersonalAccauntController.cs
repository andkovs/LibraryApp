using LibraryApp.DAL;
using LibraryApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace LibraryApp.Controllers
{
    public class PersonalAccauntController : Controller
    {
        ModelContext db = new ModelContext();

        // GET: PersonalAccaunt
        public ActionResult Authorization()
        {
            var cookie = Request.Cookies["id"];
            if (cookie != null && !cookie.Value.Equals(""))
            {
                return RedirectToAction("PersonalUserAccaunt", new { id = Int32.Parse(cookie.Value) });
            }
            return View();
        }

        [HttpPost]
        public ActionResult Authorization(UserAuthorizationModel model)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users
                    .Where(m => m.Login.Equals(model.Login))
                    .FirstOrDefault();

                if (user == null)
                {
                    return View("FailAuthorization");
                }

                UserAccountModel userAccaunt = new UserAccountModel
                {
                    Id = user.Id,
                    Login = user.Login,
                    Name = user.Name,
                    LastName = user.LastName,
                    Email = user.Email,
                    AvatarUrl = user.AvatarUrl
                };

                if (model.Password.Equals(user.Password))
                {
                    var cookie = new HttpCookie("id", user.Id.ToString());
                    Response.SetCookie(cookie);
                    cookie = new HttpCookie("role", user.Role);
                    Response.SetCookie(cookie);
                    if (user.Role.Equals("admin"))
                    {
                        return View("PersonalAdminAccaunt", userAccaunt);
                    }
                    if (user.Role.Equals("user"))
                    {
                        return View("PersonalUserAccaunt", userAccaunt);
                    }
                }
                else
                {
                    HttpContext.Response.Cookies["id"].Value = "";
                    HttpContext.Response.Cookies["role"].Value = "";
                    return View("FailAuthorization");
                }
            }
            return View();
        }

        public ActionResult PersonalUserAccaunt(int id) 
        {
            var user = db.Users
                    .Where(m => m.Id==id)
                    .FirstOrDefault();
            UserAccountModel userAccaunt = new UserAccountModel
            {
                Id = user.Id,
                Login = user.Login,
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                AvatarUrl = user.AvatarUrl
            };
            if (user.Role.Equals("admin"))
            {
                return View("PersonalAdminAccaunt", userAccaunt);
            }

            return View(userAccaunt);
        }

        public ActionResult Registry()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registry(UserRegistryModel model)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users
                    .Where(m => m.Login.Equals(model.Login))
                    .FirstOrDefault();
                if (user != null) { 
                
                }

                User newUser = new User
                {
                    Login = model.Login,
                    Password = model.Password,
                    Email = model.Email,
                    Role = "user"
                };

                db.Users.Add(newUser);
                db.SaveChanges();

                var cookie = new HttpCookie("id", newUser.Id.ToString());
                Response.SetCookie(cookie);
                cookie = new HttpCookie("role", newUser.Role);
                Response.SetCookie(cookie);

                return RedirectToAction("PersonalUserAccaunt", new { id = newUser.Id });
            }
            return View();            
        }

        public ActionResult Exit()
        {
            HttpContext.Response.Cookies["id"].Value = "";
            HttpContext.Response.Cookies["role"].Value = "";
            return RedirectToAction("Authorization");
        }
    }
}