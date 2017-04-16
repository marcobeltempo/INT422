using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment8.Controllers
{
    public class AlbumAdd
    {
        public AlbumAdd()
        {
            Coordinator = "";
            Genre = "";
            Name = "";
            ReleaseDate = DateTime.Now;
            UrlAlbum = "";
        }

        [Required, StringLength(100)]
        [Display(Name = "Album Name")]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Album Cover")]
        public string UrlAlbum { get; set; }

        [StringLength(160)]
        [Display(Name = "Primary Genre")]
        public string Genre { get; set; }

        [Key]
        public int Id { get; set; }

        public int GenreId { get; set; }

        [Required, StringLength(160)]
        [Display(Name = "Coordinator of Album")]
        public string Coordinator { get; set; }

        public IEnumerable<int> TrackIds { get; set; }

        public IEnumerable<int> ArtistIds { get; set; }
    }

    public class AlbumAddForm : AlbumAdd
    {
       
        public string KnownArtists { get; set; }
        [Display(Name = "Primary Genre")]
        public SelectList GenreList { get; set; }

        public MultiSelectList TrackList { get; set; }

        public MultiSelectList ArtistList { get; set; }

    }

    public class AlbumWithDetail : AlbumAdd
    {
        public AlbumWithDetail()
        {
            Tracks = new List<TrackAdd>();
            Artists = new List<ArtistAdd>();
        }

        public IEnumerable<String> ArtistNames { get; set; }
        public IEnumerable<ArtistAdd> Artists { get; set; }
        [Display(Name = "Tracklisting (Total: ")]
        public IEnumerable<TrackAdd> Tracks { get; set; }
    }

}
