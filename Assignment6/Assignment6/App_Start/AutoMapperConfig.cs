using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AutoMapper;

namespace Assignment6
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Models.Playlist, Controllers.PlaylistBase>();
                cfg.CreateMap<Models.Playlist, Controllers.PlaylistWithDetail>();
                cfg.CreateMap<Controllers.PlaylistBase, Controllers.PlaylistEditTrack>();
                cfg.CreateMap<Controllers.PlaylistBase, Controllers.PlaylistEditTrackForm>();
                cfg.CreateMap<Controllers.PlaylistWithDetail, Controllers.PlaylistEditTrackForm>();

                cfg.CreateMap<Models.Track, Controllers.TrackBase>();
            });
        }
    }
}