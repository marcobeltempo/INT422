using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment5.Controllers
{
    public class TrackBase
    {
        [Display(Name = "Track Name")]
        public string Name { get; set; }

        public string Composer { get; set; }

        [Display(Name = "Length (ms)")]
        public int Milliseconds { get; set; }

        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }

        [Key]
        public int TrackId { get; set; }

       

        public TrackBase()
        {
            Composer = "";
            Milliseconds = 0;
            Name = "";
            UnitPrice = 0;
        }

    }

    public class TrackWithDetail : TrackBase
    {
        [Display(Name = "Artist Name")]
        public string AlbumArtistName { get; set; }

        [Display(Name = "Album Title")]
        public string AlbumTitle { get; set; }

        [Display(Name = "Media Type")]
        public MediaTypeBase MediaType { get; set; }

        public TrackWithDetail()
        {
            AlbumArtistName = "";
            AlbumTitle = "";
 
        }
    }

    public class TrackAdd 
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        [StringLength(220)]
        public string Composer { get; set; }

        [Required]
        public int Milliseconds { get; set; }

        [Required]
        [Column(TypeName = "numeric")]
        public decimal UnitPrice { get; set; }


        [Range(1, Int32.MaxValue)]
        public int AlbumId { get; set; }

        [Range(1, Int32.MaxValue)]
        public int MediaTypeId { get; set; }

        public TrackAdd()
        {
            Milliseconds = 0;
        }
  
    }

    public class TrackAddForm : TrackAdd
    {
        [Display(Name = "Album")]
        public SelectList AlbumList { get; set; }

        public String AlbumTitle;

        [Display(Name = "MediaType")]
        public SelectList MediaTypeList { get; set; }


        public String MediaTypeTitle;


    }

}