using LibraryApp.DAL;
using LibraryApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryApp.Controllers
{
    public class OrderController : Controller
    {
        ModelContext db = new ModelContext();
        // GET: Order
        public ActionResult Index(int id)
        {
            IEnumerable<Order> orders = null;
            if (id > 0)
            {
                orders = db.Orders.Where(m => m.UserId == id);
            }
            else
            {
                orders = db.Orders;
            }
            return View(orders);
        }

        public ActionResult Statistic()
        {
            StatisticModel statistic = new StatisticModel
            {
                BookCount = db.Books.Count(),
                AuthorCount = db.Authors.Count(),
                UserCount = db.Users.Count(),
                DownloadCount = db.Orders.Count(),
                CategoryBooks = new List<CategoryBooks>(),
                AuthorBooks = new List<AuthorBooks>()
            };

            var categoryBooks = from category in db.Books
                                group category by category.Category.Title into g
                                select new { Name = g.Key, Count = g.Count() };
            foreach (var c in categoryBooks)
            {
                statistic.CategoryBooks.Add(new CategoryBooks
                {
                    CategoryTitle = c.Name,
                    BookCount = c.Count
                });
            }

            var authorBooks = from author in db.Books
                              group author by author.Author.Name into g
                              select new { Name = g.Key, Count = g.Count() };
            foreach (var c in authorBooks)
            {
                statistic.AuthorBooks.Add(new AuthorBooks
                {
                    AuthorName = c.Name,
                    BookCount = c.Count
                });
            }

            var book = db.Books.OrderByDescending(u => u.DownloadCount).First();
            statistic.PopularBook = new PopularBook
            {
                Title = book.Title,
                AuthorName = book.Author.Name,
                DownloadCount = book.DownloadCount
            };
            return View(statistic);
        }
    }
}