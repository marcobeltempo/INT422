using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment9.Controllers
{
    public class AlbumAdd
    {
        public AlbumAdd()
        {
            Coordinator = "";
            Name = "";
            ReleaseDate = DateTime.Now;
            UrlAlbum = "";
        }

        [Required]
        [Display(Name = "Album Name")]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Album Cover")]
        public string UrlAlbum { get; set; }

        public int GenreId { get; set; }

        public string Genre { get; set; }

        [StringLength(160)]
        [Display(Name = "Coordinator of Album")]
        public string Coordinator { get; set; }

        [DataType(DataType.MultilineText)]
        public string Depiction { get; set; }
    }

    public class AlbumAddForm : AlbumAdd
    {
        [Display(Name = "Primary Genre")]
        public SelectList GenreList { get; set; }

        public string GenreName { get; set; }

        [Display(Name = "Artist Name"), StringLength(200)]
        public string ArtistName { get; set; }
    }

    public class AlbumBase : AlbumAdd
    {
        public AlbumBase()
        {
        }

        public int Id { get; set; }

    }

    public class AlbumWithDetail : AlbumBase
    {
        public AlbumWithDetail()
        {

        }

        [Display(Name = "Primary Genre")]
        public string GenreName { get; set; }

        [Display(Name = "Artist Name"), StringLength(200)]
        public string ArtistName { get; set; }
    }

}
