using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment8.Controllers
{

    public class TrackAdd
    {
        public TrackAdd()
        {
            Clerk = "";
            Composers = "";
            Genre = "";
            Name = "";
        }

        
        [Required,StringLength(160)]
        [Display(Name = "Track Name")]
        public string Name { get; set; }

        [StringLength(160)]
        public string Composers { get; set; }

        [StringLength(160)]
        public string Genre { get; set; }

        [StringLength(160)]
        public string Clerk { get; set; }

        [Key]
        public int Id { get; set; }

        public int GenreId { get; set; }

    }

    public class TrackAddForm : TrackAdd
    {

        public TrackAddForm()
        {

        }

        public SelectList GenreList { get; set; }

    }
    public class TrackWithDetail : TrackAdd
    {

        public TrackWithDetail()
        {
            AlbumNames = new List<String>();
        }

        [Display(Name = "Feat Albums ( ")]
        public IEnumerable<String> AlbumNames { get; set; }
    }

    public class TrackEditForm
    {
        public int Id { get; set; }

        public string Composers { get; set; }

        public string Name { get; set; }
    }

    public class TrackEdit
    {
        public int Id { get; set; }

        public string Composers { get; set; }

        [Required]
        public string Name { get; set; }

    }
}