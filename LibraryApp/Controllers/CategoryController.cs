using LibraryApp.DAL;
using LibraryApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryApp.Controllers
{
    public class CategoryController : Controller
    {
        ModelContext db = new ModelContext();

        // GET: Category
        public ActionResult Index()
        {
            CategoryListModel model = new CategoryListModel{
                Categories = db.Categories
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

            var model = new CategoryEditModel();

            if (id > 0)
            {
                var category = db.Categories.Where(m => m.Id == id)
                    .FirstOrDefault();

                if (category != null)
                {
                    model.Id = category.Id;
                    model.Title = category.Title;
                    
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(CategoryEditModel model)
        {
            if (ModelState.IsValid)
            {
                var category = db.Categories.Where(m => m.Id == model.Id)
                    .FirstOrDefault();

                if (category != null)
                {
                    category.Title = model.Title;
                }
                else
                {
                    category = new Category
                    {
                        Title = model.Title
                    };

                    db.Categories.Add(category);
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
                var caregory = db.Categories
                    .Where(m => m.Id == id)
                    .FirstOrDefault();

                if (caregory != null)
                {
                    db.Categories.Remove(caregory);
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }


    }
}