using LibraryApp.DAL;
using LibraryApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryApp.Controllers
{
    public class BookController : Controller
    {
		ModelContext db = new ModelContext();

        public ActionResult Index(int id) 
        {
            var book = db.Books.Where(m => m.Id == id).FirstOrDefault();
            var model = new BookModel
            {
                Id = book.Id,
                Title = book.Title,
                Year = book.Year,
                Description = book.Description,
                CoverUrl = book.CoverUrl,
                Author = book.Author.Name,
                Category = book.Category.Title
            };
            if (HttpContext.Request.Cookies["role"].Value.Equals("admin"))
            {
                model.isDownload = true;
            }
            else if (HttpContext.Request.Cookies["role"].Value.Equals("user"))
            {
                var userId = Int32.Parse(HttpContext.Request.Cookies["id"].Value);
                var orders = db.Orders.Where(m => m.UserId == userId);
                model.isDownload = true;
                foreach (var o in orders)
                {
                    if (o.BookId == book.Id)
                    {
                        model.isDownload = false;
                    }
                }
            }
            else 
            {
                model.isDownload = false;
            }
            model.Role = HttpContext.Request.Cookies["role"].Value;
            return View(model);
        }

        public ActionResult Download(int id)
        {
            var order = new Order
            {
                UserId = Int32.Parse(HttpContext.Request.Cookies["id"].Value),
                BookId = id,
                Date = DateTime.Now
            };
            db.Orders.Add(order);
            var book = db.Books.Where(m => m.Id == id).FirstOrDefault();
            book.DownloadCount++;
            db.SaveChanges();
            return RedirectToAction("index", new { id = id });
        }

        public ActionResult IndexAuthor(int id)
        {
            var author = (from a in db.Authors
                         where a.Id == id 
                         select a).FirstOrDefault(); 
            BookListModel model = new BookListModel
            {
                Books = db.Books.Where(m => m.AuthorId == id),
                CategoryAuthor = author.Name
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

        public ActionResult IndexCategory(int id)
        {
            var category = (from a in db.Categories
                          where a.Id == id
                          select a).FirstOrDefault(); 
            BookListModel model = new BookListModel
            {
                Books = db.Books.Where(m => m.CategoryId == id),
                CategoryAuthor = category.Title
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

			var model = new BookEditModel();

			if (id > 0)
			{
				var book = db.Books.Where(m => m.Id == id)
					.FirstOrDefault();

				if (book != null)
				{
					model.Id = book.Id;
					model.Title = book.Title;
					model.Description = book.Description;
                    model.Year = book.Year;
					model.CoverUrl = book.CoverUrl;
					model.AuthorId = book.AuthorId;
					model.CategoryId = book.CategoryId;
				}
			}

			model.Authors = db.Authors
				.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Name, Selected = m.Id == model.AuthorId })
				.ToList();

			model.Categories = db.Categories
				.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Title, Selected = m.Id == model.CategoryId })
				.ToList();

			return View(model);
		}

		[HttpPost]
		public ActionResult Edit(BookEditModel model) 
		{
			if (ModelState.IsValid)
			{
				var book = db.Books.Where(m => m.Id == model.Id)
					.FirstOrDefault();

				if (book != null)
				{
					book.Title = model.Title;
					book.Description = model.Description;
					book.Year = model.Year;
					book.CoverUrl = model.CoverUrl;
					book.AuthorId = model.AuthorId;
                    book.CategoryId = model.CategoryId;
				}
				else
				{
					book = new Book {
						Title = model.Title,
						Description = model.Description,
						Year = model.Year,
						CoverUrl = model.CoverUrl,
						AuthorId = model.AuthorId,
                        CategoryId = model.CategoryId
					};

					db.Books.Add(book);
				}

				db.SaveChanges();
                return RedirectToAction("IndexAuthor", new { id = model.AuthorId });
			}

			model.Authors = db.Authors
				.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Name, Selected = m.Id == model.AuthorId })
				.ToList();

			model.Categories = db.Categories
				.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Title, Selected = m.Id == model.CategoryId })
				.ToList();

			return View(model);
		}

		public ActionResult Remove(int id)
		{
			if (id > 0)
			{
				var book = db.Books
					.Where(m => m.Id == id)
					.FirstOrDefault();

				if (book != null)
				{
					db.Books.Remove(book);
					db.SaveChanges();
				}
			}

			return RedirectToAction("Index");
		}
    }
}