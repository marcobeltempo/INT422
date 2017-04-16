using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Assignment8.Models;
using System.Security.Claims;

namespace Assignment8.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private ApplicationDbContext ds = new ApplicationDbContext();

        // Declare a property to hold the user account for the current request
        // Can use this property here in the Manager class to control logic and flow
        // Can also use this property in a controller 
        // Can also use this property in a view; for best results, 
        // near the top of the view, add this statement:
        // var userAccount = new ConditionalMenu.Controllers.UserAccount(User as System.Security.Claims.ClaimsPrincipal);
        // Then, you can use "userAccount" anywhere in the view to render content
        public UserAccount UserAccount { get; private set; }

        public Manager()
        {
            // If necessary, add constructor code here

            // Initialize the UserAccount property
            UserAccount = new UserAccount(HttpContext.Current.User as ClaimsPrincipal);

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;
        }

        // ############################################################
        // RoleClaim

        public List<string> RoleClaimGetAllStrings()
        {
            return ds.RoleClaims.OrderBy(r => r.Name).Select(r => r.Name).ToList();
        }

        // Add methods below
        // Controllers will call these methods
        // Ensure that the methods accept and deliver ONLY view model objects and collections
        // The collection return type is almost always IEnumerable<T>

        /* ---Track Methods--- */


        public IEnumerable<TrackAdd> TrackGetAll()
        {
            return Mapper.Map<IEnumerable<Track>, IEnumerable<TrackAdd>>(ds.Tracks.OrderBy(x => x.Name));
        }
        public IEnumerable<TrackAdd> TrackGetAllByArtistId(int id)

        {

            // Fetch the artist
            var o = ds.Artists.Include("Albums.Tracks").SingleOrDefault(a => a.Id == id);
            
            // Continue?
            if (o == null) { return null; }

            // Create a collection to hold the results
            var c = new List<Track>();

            // Go through each album, and get the tracks
            foreach (var album in o.Albums)
            {
                c.AddRange(album.Tracks);
            }

            // Remove duplicates
            c = c.Distinct().ToList();

            return Mapper.Map<IEnumerable<Track>, IEnumerable<TrackAdd>>(c.OrderBy(t => t.Name));
        }

        public TrackWithDetail TrackGetByIdWithDetail(int id)
        {
            var o = ds.Tracks.Include("Albums.Artists").SingleOrDefault(t => t.Id == id);



            // Create the result collection

            var result = Mapper.Map<Track, TrackWithDetail>(o);

            // Fill in the album names
            result.AlbumNames = o.Albums.Select(x=> x.Name); 
            return result;
           // return (o == null) ? null : Mapper.Map<Track, TrackWithDetail>(o);
        }

        public TrackAdd TrackAdd(TrackAdd newItem)
        {
            var addedItem = ds.Tracks.Add(Mapper.Map<TrackAdd, Track>(newItem));
            var findGenre = ds.Genres.Find(newItem.GenreId);

            addedItem.Genre = findGenre.Name;
            ds.SaveChanges();

            return (addedItem == null) ? null : Mapper.Map<Track, TrackAdd>(addedItem);
        }

        public TrackWithDetail TrackEdit(TrackEdit newItem)
        {
            var o = ds.Tracks.Include("Albums").SingleOrDefault(x => x.Id == newItem.Id);

            if (o == null)
            {
                return null;
            }
            else
            {
                ds.Entry(o).CurrentValues.SetValues(newItem);
                ds.SaveChanges();

                return Mapper.Map<Track, TrackWithDetail>(o);
            }
        }

        public bool TrackDelete(int id)
        {
            var itemToDelete = ds.Tracks.Find(id);

            if (itemToDelete == null)
            {
                return false;
            }
            else
            {
                ds.Tracks.Remove(itemToDelete);
                ds.SaveChanges();

                return true;
            }
        }
        /* -!- End Track Methods-!- */

        /* --- Album Methods--- */
        public IEnumerable<AlbumAdd> AlbumGetAll()
        {
            return Mapper.Map<IEnumerable<Album>, IEnumerable<AlbumAdd>>(ds.Albums.OrderBy(x => x.Name));
        }


        public AlbumWithDetail AlbumGetByIdWithDetail(int id)
        {
            var o = ds.Albums.Include("Artists").Include("Tracks").SingleOrDefault(x => x.Id == id);
            if (o == null)
            {
                return null;
            }
            else
            {
                // Create the result collection
                var result = Mapper.Map<Album, AlbumWithDetail>(o);
                // Fill in the album names
                result.ArtistNames = o.Artists.Select(x => x.Name);
                return result;
            }
        }

        public AlbumAdd AlbumAdd(AlbumAdd newItem)
        {
            // Attempt to add the new item
            var addedItem = ds.Albums.Add(Mapper.Map<AlbumAdd, Album>(newItem));
            var findGenre = ds.Genres.Find(newItem.GenreId);

            foreach (var item in newItem.TrackIds)
            {
                var a = ds.Tracks.Find(item);
                addedItem.Tracks.Add(a);
            }

            foreach (var item in newItem.ArtistIds)
            {
                var a = ds.Artists.Find(item);
                addedItem.Artists.Add(a);
            }

            // Set the associated item property
            addedItem.Genre = findGenre.Name;
            ds.SaveChanges();

            return (addedItem == null) ? null : Mapper.Map<Album, AlbumAdd>(addedItem);
        }


        /* -!- End Album Methods-!- */

        /* ---Artist Methods--- */

        public IEnumerable<ArtistAdd> ArtistGetAll()
        {
            return Mapper.Map<IEnumerable<Artist>, IEnumerable<ArtistAdd>>(ds.Artists.OrderBy(x => x.Name));
        }

        public ArtistWithDetail ArtistGetByIdWithDetail(int id)
        {
            var o = ds.Artists.Include("Albums").SingleOrDefault(a => a.Id == id);
            return (o == null) ? null : Mapper.Map<Artist, ArtistWithDetail>(o);
        }

        public ArtistAdd ArtistAdd(ArtistAdd newItem)
        {
            var addedItem = ds.Artists.Add(Mapper.Map<ArtistAdd, Artist>(newItem));
            var findGenre = ds.Genres.Find(newItem.GenreId);

            addedItem.Genre = findGenre.Name;
            ds.SaveChanges();

            return (addedItem == null) ? null : Mapper.Map<Artist, ArtistAdd>(addedItem);
        }

        /* -!-End Artist Methods-!- */

        /* --- Genre Methods--- */

        public IEnumerable<GenreBase> GenreGetAll()
        {
            return Mapper.Map<IEnumerable<Genre>, IEnumerable<GenreBase>>(ds.Genres.OrderBy(x => x.Name));
        }

        /* -!-End Genre Methods-!- */


        // Add some programmatically-generated objects to the data store
        // Can write one method, or many methods - your decision
        // The important idea is that you check for existing data first
        // Call this method from a controller action/method

        public bool LoadDataRoleClaim()
        {
            if (ds.RoleClaims.Count() == 0)
            {
                ds.RoleClaims.Add(new RoleClaim { Name = "Executive" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Coordinator" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Clerk" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Staff" });

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool LoadDataGenre()
        {

            if (ds.Genres.Count() > 0) { return false; }

            else
            {
                ds.Genres.Add(new Genre { Name = "Hip-Hop" });
                ds.Genres.Add(new Genre { Name = "R & B" });
                ds.Genres.Add(new Genre { Name = "Rock" });
                ds.Genres.Add(new Genre { Name = "Alternative Rock" });
                ds.Genres.Add(new Genre { Name = "Indie" });
                ds.Genres.Add(new Genre { Name = "Country" });
                ds.Genres.Add(new Genre { Name = "Classical" });
                ds.Genres.Add(new Genre { Name = "Dubstep" });
                ds.Genres.Add(new Genre { Name = "Electronic" });
                ds.Genres.Add(new Genre { Name = "Techno" });

                ds.SaveChanges();
                return true;
            }
        }

        public bool LoadDataArtist()
        {

            if (ds.Artists.Count() > 0) { return false; }

            else
            {
                var hipHop = ds.Genres.SingleOrDefault(a => a.Name == "Hip-Hop");
                var indie = ds.Genres.SingleOrDefault(a => a.Name == "Indie");
                var rock = ds.Genres.SingleOrDefault(a => a.Name == "Rock");

                ds.Artists.Add(new Artist
                {
                    Name = "Logic",
                    BirthName = "Bobby Smith",
                    BirthOrStartDate = new DateTime(1992, 2, 7),
                    Executive = "John Smith",
                    Genre = hipHop.Name,
                    UrlArtist = "https://goo.gl/U78Yzu"
                });

                ds.Artists.Add(new Artist
                {
                    Name = "Sam Roberts Band",
                    BirthName = "Sam Roberts",
                    BirthOrStartDate = new DateTime(1972, 7, 20),
                    Executive = "Bob Dowe",
                    Genre = indie.Name,
                    UrlArtist = "https://goo.gl/4RYbvL"
                });

                ds.Artists.Add(new Artist
                {
                    Name = "City and Colour",
                    BirthName = "Dallas Green",
                    BirthOrStartDate = new DateTime(1982, 9, 10),
                    Executive = "Jane Doe",
                    Genre = rock.Name,
                    UrlArtist = "https://goo.gl/jtjUDB"
                });

                ds.SaveChanges();
                return true;
            }
        }

        public bool LoadDataAlbum()
        {

            if (ds.Albums.Count() > 0) { return false; }

            else
            {
                var logic = ds.Artists.SingleOrDefault(a => a.Name == "Logic");
                var hipHop = ds.Genres.SingleOrDefault(a => a.Name == "Hip-Hop");

                var sinatra = ds.Albums.Add(new Album
                {
                    Name = "Young Sinatra",
                    Coordinator = "Bob Smith",
                    Genre = hipHop.Name,
                    ReleaseDate = new DateTime(2014, 8, 18),
                    Artists = new List<Artist> { logic },
                    UrlAlbum = "https://goo.gl/P1EYyZ"
                });

                var pressure = ds.Albums.Add(new Album
                {
                    Name = "Under Pressure",
                    Coordinator = "6ix",
                    Genre = hipHop.Name,
                    ReleaseDate = new DateTime(2015, 10, 4),
                    Artists = new List<Artist> { logic },
                    UrlAlbum = "https://goo.gl/mge1M5"
                });

                ds.SaveChanges();
                return true;
            }
        }
        public bool LoadDataTrack()
        {

            if (ds.Tracks.Count() > 0) { return false; }

            else
            {
                var logic = ds.Artists.SingleOrDefault(a => a.Name == "Logic");
                var hipHop = ds.Genres.SingleOrDefault(a => a.Name == "Hip-Hop");
                var sinatra = ds.Albums.SingleOrDefault(a => a.Name == "Young Sinatra");
                var pressure = ds.Albums.SingleOrDefault(a => a.Name == "Under Pressure");

                ds.Tracks.Add(new Track
                {
                    Name = "Addiction",
                    Clerk = "bob@example.com",
                    Composers = logic.Name,
                    Genre = hipHop.Name,
                    Albums = new List<Album> { sinatra }
                });

                ds.Tracks.Add(new Track
                {
                    Name = "All I do",
                    Clerk = "bob@example.com",
                    Composers = logic.Name,
                    Genre = hipHop.Name,
                    Albums = new List<Album> { sinatra }
                });

                ds.Tracks.Add(new Track
                {
                    Name = "Are You Ready",
                    Clerk = "bob@example.com",
                    Composers = logic.Name,
                    Genre = hipHop.Name,
                    Albums = new List<Album> { sinatra }
                });

                ds.Tracks.Add(new Track
                {
                    Name = "As I Am",
                    Clerk = "bob@example.com",
                    Composers = logic.Name,
                    Genre = hipHop.Name,
                    Albums = new List<Album> { sinatra }
                });

                ds.Tracks.Add(new Track
                {
                    Name = "Beggin",
                    Clerk = "bob@example.com",
                    Composers = logic.Name,
                    Genre = hipHop.Name,
                    Albums = new List<Album> { sinatra }
                });

                ds.Tracks.Add(new Track
                {
                    Name = "Soul Foo D",
                    Clerk = "john@example.com",
                    Composers = logic.Name,
                    Genre = hipHop.Name,
                    Albums = new List<Album> { pressure }
                });

                ds.Tracks.Add(new Track
                {
                    Name = "Im Gone",
                    Clerk = "john@example.com",
                    Composers = logic.Name,
                    Genre = hipHop.Name,
                    Albums = new List<Album> { pressure }
                });

                ds.Tracks.Add(new Track
                {
                    Name = "Bounce",
                    Clerk = "john@example.com",
                    Composers = logic.Name,
                    Genre = hipHop.Name,
                    Albums = new List<Album> { pressure }
                });

                ds.Tracks.Add(new Track
                {
                    Clerk = "john@example.com",
                    Composers = logic.Name,
                    Genre = hipHop.Name,
                    Name = "Never Enough",
                    Albums = new List<Album> { pressure }
                });

                ds.Tracks.Add(new Track
                {
                    Name = "Metropolis",
                    Clerk = "john@example.com",
                    Composers = logic.Name,
                    Genre = hipHop.Name,
                    Albums = new List<Album> { pressure }
                });

                ds.SaveChanges();
                return true;
            }
        }

        public bool RemoveData()
        {
            try
            {
                foreach (var e in ds.RoleClaims)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveDatabase()
        {
            try
            {
                return ds.Database.Delete();
            }
            catch (Exception)
            {
                return false;
            }
        }

    }

    // New "UserAccount" class for the authenticated user
    // Includes many convenient members to make it easier to render user account info
    // Study the properties and methods, and think about how you could use it
    public class UserAccount
    {
        // Constructor, pass in the security principal
        public UserAccount(ClaimsPrincipal user)
        {
            if (HttpContext.Current.Request.IsAuthenticated)
            {
                Principal = user;

                // Extract the role claims
                RoleClaims = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

                // User name
                Name = user.Identity.Name;

                // Extract the given name(s); if null or empty, then set an initial value
                string gn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.GivenName).Value;
                if (string.IsNullOrEmpty(gn)) { gn = "(empty given name)"; }
                GivenName = gn;

                // Extract the surname; if null or empty, then set an initial value
                string sn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Surname).Value;
                if (string.IsNullOrEmpty(sn)) { sn = "(empty surname)"; }
                Surname = sn;

                IsAuthenticated = true;
                IsAdmin = user.HasClaim(ClaimTypes.Role, "Admin") ? true : false;
            }
            else
            {
                RoleClaims = new List<string>();
                Name = "anonymous";
                GivenName = "Unauthenticated";
                Surname = "Anonymous";
                IsAuthenticated = false;
                IsAdmin = false;
            }

            NamesFirstLast = $"{GivenName} {Surname}";
            NamesLastFirst = $"{Surname}, {GivenName}";
        }

        // Public properties
        public ClaimsPrincipal Principal { get; private set; }
        public IEnumerable<string> RoleClaims { get; private set; }

        public string Name { get; set; }

        public string GivenName { get; private set; }
        public string Surname { get; private set; }

        public string NamesFirstLast { get; private set; }
        public string NamesLastFirst { get; private set; }

        public bool IsAuthenticated { get; private set; }

        // Add other role-checking properties here as needed
        public bool IsAdmin { get; private set; }

        public bool HasRoleClaim(string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(ClaimTypes.Role, value) ? true : false;
        }

        public bool HasClaim(string type, string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(type, value) ? true : false;
        }

    }

}