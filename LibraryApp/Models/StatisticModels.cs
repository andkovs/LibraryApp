using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryApp.Models
{
    public class StatisticModel
    {
        public int BookCount { get; set; }

        public int UserCount { get; set; }

        public int DownloadCount { get; set; }

        public int AuthorCount { get; set; }

        public List<CategoryBooks> CategoryBooks { get; set; }

        public List<AuthorBooks> AuthorBooks { get; set; }

        public PopularBook PopularBook { get; set; }
    }

    public class CategoryBooks
    {
        public String CategoryTitle { get; set; }

        public int BookCount { get; set; }
    }

    public class AuthorBooks
    {
        public String AuthorName { get; set; }

        public int BookCount { get; set; }
    }

    public class PopularBook
    {
        public String Title { get; set; }

        public String AuthorName { get; set; }

        public int DownloadCount { get; set; }
    }
}