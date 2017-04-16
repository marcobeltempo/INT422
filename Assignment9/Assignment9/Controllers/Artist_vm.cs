using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment9.Controllers
{
    public class ArtistBase : ArtistAdd
    {
        public ArtistBase()
        {

        }

        public int Id { get; set; }
    }

    public class ArtistAdd
    {
        public ArtistAdd()
        {
            BirthName = "";
            BirthOrStartDate = DateTime.Now;
            Executive = "";
            Name = "";
            UrlArtist = "";
        }

        [Required]
        [Display(Name = "Artist Name or Stage Name")]
        public string Name { get; set; }

        [Display(Name = "Birth Name")]
        public string BirthName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Birth Date or Start Date")]
        public DateTime BirthOrStartDate { get; set; }

        [Display(Name = "Artist Photo")]
        public string UrlArtist { get; set; }

        [Display(Name = "Primary Genre")]
        public string Genre { get; set; }

        public int GenreId { get; set; }

        public string Executive { get; set; }

        [DataType(DataType.MultilineText)]
        public string Portrayal { get; set; }

    }

    public class ArtistAddForm : ArtistAdd
    {
        public ArtistAddForm()
        {

        }

        public SelectList GenreList { get; set; }

    }

    public class ArtistWithDetail : ArtistBase
    {
        public ArtistWithDetail()
        {
            MediaItems = new List<MediaItemBase>();
        }
        public IEnumerable<MediaItemBase> MediaItems { get; set; }
    }

}