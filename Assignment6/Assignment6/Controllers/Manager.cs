using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Assignment6.Models;

namespace Assignment6.Controllers
{
    public class Manager
    {
        private DataContext ds = new DataContext();

        public Manager()
        {
            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;
        }

        public IEnumerable<PlaylistBase> PlayListGetAll()
        {
            return Mapper.Map<IEnumerable<Playlist>, IEnumerable<PlaylistBase>>(ds.Playlists.OrderBy(x => x.Name));
        }

        public PlaylistWithDetail PlaylistGetByIdWithDetail(int id)
        {
            //fetch the associated Track objects
            var o = ds.Playlists.Include("Tracks").SingleOrDefault(x => x.PlaylistId == id);
            return (o == null) ? null : Mapper.Map<Playlist, PlaylistWithDetail>(o);
        }

        public IEnumerable<PlaylistWithDetail> PlaylistGetAllWithDetail()
        {
            var o = ds.Playlists.Include("Tracks").OrderBy(x => x.PlaylistId);
            return Mapper.Map<IEnumerable<Playlist>, IEnumerable<PlaylistWithDetail>>(o);
        }

        public IEnumerable<TrackBase> TrackGetAll()
        {
            return Mapper.Map<IEnumerable<Track>, IEnumerable<TrackBase>>(ds.Tracks.OrderBy(x => x.Name));
        }

        public PlaylistWithDetail PlaylistEditTracks(PlaylistEditTrack newItem)
        {
            var o = ds.Playlists.Include("Tracks").SingleOrDefault(t => t.PlaylistId == newItem.Id);

            if (o == null) { return null; }

            else
            {
                o.Tracks.Clear();

                foreach (var track in newItem.TrackIds)
                {
                    var a = ds.Tracks.Find(track);
                    o.Tracks.Add(a);
                }
                ds.SaveChanges();

                return Mapper.Map<Playlist, PlaylistWithDetail>(o);
            }
        }
    }
}