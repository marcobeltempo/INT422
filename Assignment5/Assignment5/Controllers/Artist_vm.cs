using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment5.Controllers
{
    public class ArtistBase
    {
        [Key]
        public int ArtistId { get; set; }
        public string Name { get; set; }

    public ArtistBase()
        {
            Name = "";
        }
    }
}