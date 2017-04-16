using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment8.Controllers
{
    public class GenreBase
    {
        public GenreBase()
        {
            Name = "";
        }
        public int Id { get; set; }

        [Required, StringLength(160)]
        public string Name { get; set; }

        public SelectList Genres { get; set; }
    }
}