using LibraryApp.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryApp.Models
{
    public class BookModel 
    {
        public int Id { get; set; }

        public String Title { get; set; }

        public String Description { get; set; }

        public String Year { get; set; }

        public String CoverUrl { get; set; }

        public String Author { get; set; }

        public String Category { get; set; }

        public bool isDownload { get; set; }

        public string Role { get; set; }
    }

    public class BookListModel
    {
        public IEnumerable<Book> Books { get; set; }

        public string CategoryAuthor { get; set; }

        public string Role { get; set; }
    }

	public class BookEditModel
	{
		public int Id { get; set; }

		[Required]
		public string Title { get; set; }

		public string Description { get; set; }

		[Required]
        public String Year { get; set; }

		public String CoverUrl { get; set; }

		public int AuthorId { get; set; }

		public int CategoryId { get; set; }

		// additional

		public List<SelectListItem> Authors { get; set; }

		public List<SelectListItem> Categories { get; set; }
	}
}