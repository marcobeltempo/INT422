using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment6.Controllers
{

    public class PlaylistBase
    {
        [Display(Name = "Playlist Name")]
        [StringLength(120)]
        public string Name { get; set; }

        [Key]
        [Range(1, Int32.MaxValue)]
        public int PlaylistId { get; set; }

        [Display(Name = "Number of Tracks on This Playlist")]
        public int TracksCount { get; set; }

        public PlaylistBase()
        {
            Name = "";
            TracksCount = 0;
        }
    }
    public class PlaylistEditTrackForm
    {
        [Range(1, Int32.MaxValue)]
        [ScaffoldColumn(false)]
        public int PlaylistId { get; set; }

        [ScaffoldColumn(false)]
        [StringLength(120)]
        public string Name { get; set; }

        public int TracksCount { get; set; }

        public MultiSelectList TrackList { get; set; }

        public IEnumerable<TrackBase> TrackOnPlaylist { get; set; }

        public PlaylistEditTrackForm()
        {

        }
    }

    public class PlaylistEditTrack
    {
        [Range(1, Int32.MaxValue)]
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        public IEnumerable<int> TrackIds { get; set; }

        public PlaylistEditTrack()
        {
            TrackIds = new List<int>();
        }
    }

    public class PlaylistWithDetail : PlaylistBase
    {
        public IEnumerable<TrackBase> Tracks { get; set; }
    }
}

