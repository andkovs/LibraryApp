using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LibraryApp.DAL
{
    public class User
    {
        public int Id { get; set; }

        public String Login { get; set; }

        public String Password { get; set; }

        public String Role { get; set; }

        public String Name { get; set; }

        public String LastName { get; set; }

        public String Email { get; set; }

        public String AvatarUrl { get; set; }
    }

    public class Book
    {
        [Key]
        public int Id { get; set; }

        public String Title { get; set; }

        public String Description { get; set; }

        public String Year { get; set; }

        public String CoverUrl { get; set; }

        public int AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public virtual Author Author { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public int DownloadCount { get; set; }
    }

    public class Author
    {
        [Key]
        public int Id { get; set; }

        public String Name { get; set; }

        public virtual IList<Book> Books { get; set; }
    }

    public class Category
    {
        [Key]
        public int Id { get; set; }

        public String Title { get; set; }

        public virtual IList<Book> Books { get; set; }
    }

    public class Order
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public int BookId { get; set; }

        [ForeignKey("BookId")]
        public virtual Book Book { get; set; }

        public DateTime Date { get; set; }
    }
}