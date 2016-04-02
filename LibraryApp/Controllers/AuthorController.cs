using LibraryApp.DAL;
using LibraryApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryApp.Controllers
{
    public class AuthorController : Controller
    {
        ModelContext db = new ModelContext();

        // GET: Category
        public ActionResult Index()
        {
            AuthorListModel model = new AuthorListModel
            {
                Authors = db.Authors
            };
            if (HttpContext.Request.Cookies["role"].Value.Equals("admin"))
            {
                model.Role = "admin";
            }
            else
            {
                model.Role = "user";
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {

            var model = new AuthorEditModel();

            if (id > 0)
            {
                var author = db.Authors.Where(m => m.Id == id)
                    .FirstOrDefault();

                if (author != null)
                {
                    model.Id = author.Id;
                    model.Name = author.Name;

                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(AuthorEditModel model)
        {
            if (ModelState.IsValid)
            {
                var authors = db.Authors.Where(m => m.Id == model.Id)
                    .FirstOrDefault();

                if (authors != null)
                {
                    authors.Name = model.Name;
                }
                else
                {
                    authors = new Author
                    {
                        Name = model.Name
                    };

                    db.Authors.Add(authors);
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult Remove(int id)
        {
            if (id > 0)
            {
                var author = db.Authors
                    .Where(m => m.Id == id)
                    .FirstOrDefault();

                if (author != null)
                {
                    db.Authors.Remove(author);
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }
    }
}