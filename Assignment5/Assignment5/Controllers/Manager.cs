using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Assignment5.Models;

namespace Assignment5.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private DataContext ds = new DataContext();

        public Manager()
        {

        }

        public IEnumerable<AlbumBase> AlbumGetAll()
        {
            return Mapper.Map<IEnumerable<Album>, IEnumerable<AlbumBase>>(ds.Albums.OrderBy(x => x.AlbumId));
        }

        public AlbumBase AlbumGetById(int id)
        {
            var o = ds.Albums.Find(id);
            return (o == null) ? null : Mapper.Map<Album, AlbumBase>(o);
        }

        public IEnumerable<ArtistBase> ArtistGetAll()
        {
            return Mapper.Map<IEnumerable<Artist>, IEnumerable<ArtistBase>>(ds.Artists.OrderBy(x => x.ArtistId));
        }

        public IEnumerable<MediaTypeBase> MediaTypeGetAll()
        {
            return Mapper.Map<IEnumerable<MediaType>, IEnumerable<MediaTypeBase>>(ds.MediaTypes.OrderBy(x => x.MediaTypeId));
        }

        public MediaTypeBase MediaTypeGetById(int id)
        {
            var o = ds.MediaTypes.Find(id);
            return (o == null) ? null : Mapper.Map<MediaType, MediaTypeBase>(o);
        }

        public IEnumerable<TrackWithDetail> TrackGetAllWithDetail()
        {
            return Mapper.Map<IEnumerable<Track>, IEnumerable<TrackWithDetail>>(ds.Tracks.Include("MediaType").Include("Album").OrderBy(x => x.Name));
        }

        public TrackWithDetail TrackGetByIdWithDetail(int id)
        {
            var o = ds.Tracks.Include("MediaType").Include("Album").SingleOrDefault(p => p.TrackId == id);
            return (o == null) ? null : Mapper.Map<TrackWithDetail>(o);
        }

        public TrackBase TrackGetById(int id)
        {
            var o = ds.Tracks.Find(id);
            return (o == null) ? null : Mapper.Map<TrackBase>(o);
        }

        public TrackWithDetail TrackAdd(TrackAdd newTrack)
        {
            var a = ds.MediaTypes.Find(newTrack.MediaTypeId);
            var b = ds.Albums.Find(newTrack.AlbumId);

            if (a == null || b == null)
            {
                return null;
            }
            else
            {
                // Attempt to add the new item
                var addedTrack = ds.Tracks.Add(Mapper.Map<TrackAdd, Track>(newTrack));
                addedTrack.MediaType = a;
                addedTrack.Album = b;
                ds.SaveChanges();

                // If successful, return the added item, mapped to a view model object
                return (addedTrack == null) ? null : Mapper.Map<Track, TrackWithDetail>(addedTrack);
            }
        }
    }
}


