using LibraryApp.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryApp.Models
{
    public class AuthorListModel
    {
        public IEnumerable<Author> Authors { get; set; }

        public string Role { get; set; }
    }

    public class AuthorEditModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}