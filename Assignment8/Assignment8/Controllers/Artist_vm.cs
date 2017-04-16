using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment8.Controllers
{
    public class ArtistAdd
    {
        public ArtistAdd()
        {
            BirthName = "";
            BirthOrStartDate = DateTime.Now;
            Executive = "";
            Genre = "";
            Name = "";
            UrlArtist = "";
        }
        [Required, StringLength(160)]
        [Display(Name = "Artist Name or Stage Name")]
        public string Name { get; set; }

        [StringLength(160)]
        [Display(Name = "Birth Name")]
        public string BirthName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Birth Date or Start Date")]
        public DateTime BirthOrStartDate { get; set; }

        [StringLength(160)]
        public string Executive { get; set; }

        [StringLength(160)]
        [Display(Name = "Primary Genre")]
        public string Genre { get; set; }

        [Key]
        public int Id { get; set; }

        public int GenreId { get; set; }

        [Display(Name = "Artist Photo")]
        public string UrlArtist { get; set; }
    }

    public class ArtistAddForm : ArtistAdd
    {
        public SelectList GenreList { get; set; }
    }

    public class ArtistWithDetail : ArtistAdd
    {

        public ArtistWithDetail()
        {
            Albums = new List<AlbumAdd>();
        }
        public IEnumerable<AlbumAdd> Albums { get; set; }
    }
}