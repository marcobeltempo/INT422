using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment5.Controllers
{
    public class AlbumBase
    {
        [Key]
        public int AlbumId { get; set; }
        public int ArtistId { get; set; }
        public string Title { get; set; }

        public AlbumBase()
        {
            Title = "";
        }  
    }
}