using Assignment9.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment9.Controllers
{ 
    public class TrackBase : TrackAdd
    {
        public int Id { get; set; }
    }

    public class TrackAdd
    {
        public TrackAdd()
        {
            Clerk = "";
            Composers = "";
            Genre = "";
            Name = "";
            AlbumName = "";
        }
        [Display(Name = "Clerk who helps with album tasks")]
        public string Clerk { get; set; }

        [Display(Name = "Composer(s)")]
        public string Composers { get; set; }

        [Display(Name = "Track Genre")]
        public string Genre { get; set; }

        public int GenreId { get; set; }

        [Required]
        [Display(Name = "Track Name")]
        public string Name { get; set; }

        public string AlbumName { get; set; }

        [Required]
        [Display(Name = "Sample Clip")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase AudioUpload { get; set; }

    }

    public class TrackAddForm : TrackAdd
    {
        public TrackAddForm()
        {
            AudioUpload = "";
        }

        public SelectList GenreList { get; set; }

        [Required]
        [Display(Name = "Sample Clip")]
        [DataType(DataType.Upload)]
        public new string AudioUpload { get; set; }
    }

    public class TrackAudio
    {
        public TrackAudio()
        {

        }

        public int Id { get; set; }
        public string AudioContentType { get; set; }
        public byte[] Audio { get; set; }
    }

    public class TrackWithDetail : TrackBase
    {

    }

    public class TrackEdit 
    {
        public TrackEdit()
        {
            Clerk = "";
        }

        [Required]
        [Display(Name = "Sample Clip")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase AudioUpload { get; set; }
        public int Id { get; set; }
        public string Clerk { get; set; }

    }

    public class TrackEditForm : TrackEdit
    { 
        public TrackEditForm()
        {

        }

        [Required]
        [Display(Name = "Sample Clip")]
        [DataType(DataType.Upload)]
        public new string AudioUpload { get; set; }
    }

   
}