using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment9.Controllers
{
    public class MediaItemContent
    {
        public MediaItemContent()
        {
            ContentType = "";
        }

        public int Id { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
    }

    public class MediaItemBase : MediaItemContent
    {
        public MediaItemBase()
        {
            Timestamp = DateTime.Now;
            Caption = "";
        }

        [Display(Name = "Added on date/time")]
        public DateTime Timestamp { get; set; }

        [Required, StringLength(100)]
        public string StringId { get; set; }
  
        [Required, StringLength(100)]
        [Display(Name = "Descriptive Caption")]
        public string Caption { get; set; }
    }

    public class MediaItemAdd
    {
        public MediaItemAdd()
        {
            Caption = "";
        }

        public int ArtistId { get; set; }

        [Required, StringLength(100)]
        public string Caption { get; set; }

        [Required]
        public HttpPostedFileBase MediaItemUpload { get; set; }
    }

    public class MediaItemAddForm : MediaItemAdd
    {
        public MediaItemAddForm()
        {
            ArtistInfo = "";
            MediaItemUpload = "";

        }

        [Display(Name = "Artist Information")]
        public string ArtistInfo { get; set; }

        [Required]
        [Display(Name = "Media Item")]
        [DataType(DataType.Upload)]
        public new string MediaItemUpload { get; set; }
    }

}

