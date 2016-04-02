using LibraryApp.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryApp.Models
{
    public class CategoryListModel
    {
        public IEnumerable<Category> Categories { get; set; }

        public string Role { get; set; }
    }

    public class CategoryEditModel
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}