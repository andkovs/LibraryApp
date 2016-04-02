using LibraryApp.DAL;
using LibraryApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryApp.Controllers
{
    public class HomeController : Controller
    {
        ModelContext db = new ModelContext();

        // GET: Home
        public ActionResult Index()
        {
            return View();               
        }

        public ActionResult Create() 
        {
            var cookie = Request.Cookies["id"];
            if (cookie == null)
            {
                cookie = new HttpCookie("id", "");
                Response.SetCookie(cookie);
            }
            cookie = Request.Cookies["role"];
            if (cookie == null)
            {
                cookie = new HttpCookie("role", "");
                Response.SetCookie(cookie);
            }
            db.Database.Delete();
            db.Database.Create();
            db.Users.Add(new User
            {
                Id = 1,
                Login = "Admin",
                Password = "Admin",
                Name = "Андрей",
                LastName = "Ковшов",
                Email = "admin@ya.ru",
                Role = "admin"
            });
            db.Users.Add(new User
            {
                Id = 2,
                Login = "User",
                Password = "User",
                Name = "Андрей",
                LastName = "Ковшов",
                Email = "email@ya.ru",
                Role = "user"
            });

            db.Categories.Add(new Category
            {
                Id = 1,
                Title = "Ужасы"                
            });
            db.Categories.Add(new Category
            {
                Id = 2,
                Title = "Фантастика"
            });
            
            db.Authors.Add(new Author
            {
                Id = 1,
                Name = "Стивен Кинг"
            });
            db.Authors.Add(new Author
            {
                Id = 2,
                Name = "Эдгар Алан По"
            });
            db.Authors.Add(new Author
            {
                Id = 3,
                Name = "Гарри Гаррисон"
            });
            db.Authors.Add(new Author
            {
                Id = 4,
                Name = "Рей Бредбери"
            });
            
            db.Books.Add(new Book
            {
                Id = 1,
                Title = "Кэрри",
                Description = "Небольшой провинциальный город в Новой Англии в один момент становится «мертвым». На улицах повсюду лежат трупы, а над домами зверски пылает огонь. Весь этот ужас — дело рук одной запуганной, жалкой девушки Кэрри.",
                Year = "1974",
                AuthorId = 1,
                CategoryId = 1,
                CoverUrl = "1.jpg"
            });
            db.Books.Add(new Book
            {
                Id = 2,
                Title = "Кристина",
                Description = "Кристина — не женщина, но Эрни Каннингейм любит ее до безумия. Кристина — не женщина, но подруга Эрни с первого взгляда понимает, что это ее соперница.",
                Year = "1983",
                AuthorId = 1,
                CategoryId = 1,
                CoverUrl = "2.jpg"
            });
            db.Books.Add(new Book
            {
                Id = 3,
                Title = "Ворон",
                Description = "Как-то в полночь, в час угрюмый, утомившись от раздумий, Задремал я над страницей фолианта одного",
                Year = "1845",
                AuthorId = 2,
                CategoryId = 1,
                CoverUrl = "3.jpg"
            });
            db.Books.Add(new Book
            {
                Id = 4,
                Title = "Золотой жук",
                Description = "Некий отшельник — потомок благородной, но обедневшей семьи — Легран все свое время проводит в ловле рыб и насекомых. И вот однажды ему удалось поймать необычного жука.",
                Year = "1843",
                AuthorId = 2,
                CategoryId = 1,
                CoverUrl = "4.jpg"
            });
            db.Books.Add(new Book
            {
                Id = 5,
                Title = "Мир смерти",
                Description = "Каково это – быть колонистом и биться за выживание со всей планетой сразу? Планета Пирр не оставляет поселенцам ни малейшего шанса, набрасываясь на них...",
                Year = "1968",
                AuthorId = 3,
                CategoryId = 2,
                CoverUrl = "5.jpg"
            });
            db.Books.Add(new Book
            {
                Id = 6,
                Title = "Стальная крыса",
                Description = "Крыса из нержавеющей стали? Очень интересно, и что же это за зверек такой, спросит каждый, кто не знаком с удивительным героем уникального цикла произведений Гарри Гаррисона с одноименным названием.",
                Year = "1961",
                AuthorId = 3,
                CategoryId = 2,
                CoverUrl = "6.jpg"
            });
            db.Books.Add(new Book
            {
                Id = 7,
                Title = "451 градус по Фаренгейту",
                Description = "451 градус по Фаренгейту — температура, при которой воспламеняется и горит бумага. Главный герой — Монтэг — пожарник, но смысл этой профессии давно изменился.",
                Year = "1953",
                AuthorId = 4,
                CategoryId = 2,
                CoverUrl = "7.jpg"
            });
            db.Books.Add(new Book
            {
                Id = 8,
                Title = "Марсианские хроники",
                Description = "Марсианские хроники» в летописной форме описывает историю колонизации людьми Марса...",
                Year = "1950",
                AuthorId = 4,
                CategoryId = 2,
                CoverUrl = "8.jpg"
            });
            db.Orders.Add(new Order
            {
                Id = 1,
                UserId = 2,
                BookId = 1,
                Date = DateTime.Now
            });


            db.SaveChanges();

            return View("StartPage");
        }

        public ActionResult StartPage()
        {
            var cookie = Request.Cookies["id"];
            if (cookie == null)
            {
                cookie = new HttpCookie("id", "");
                Response.SetCookie(cookie);               
            }
            cookie = Request.Cookies["role"];
            if (cookie == null)
            {
                cookie = new HttpCookie("role", "");
                Response.SetCookie(cookie);
            }
            return View();
        }
    }
}