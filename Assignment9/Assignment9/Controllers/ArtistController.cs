using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment9.Controllers
{
    public class ArtistController : Controller
    {

        Manager m = new Manager();

        [AllowAnonymous]
        // GET: Artist
        public ActionResult Index()
        {
            return View(m.ArtistGetAll());
        }

        // GET: Artist/Details/5
        public ActionResult Details(int? id)
        {
            // Attempt to get the matching object
            var o = m.ArtistGetByIdWithDetail(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Pass the object to the view
                return View(o);
            }
        }

        // GET: Properties/DetailsWithPhotoInfo/5
        public ActionResult DetailsWithMediaItemInfo(int? id)
        {
            // Attempt to get the matching object
            var o = m.ArtistGetByIdWithDetail(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Pass the object to the view
                return View(o);
            }
        }

        // GET: Artist/Create
        [Authorize(Roles = "Executive")]
        public ActionResult Create()
        {
            var form = new ArtistAddForm();
            form.GenreList = new SelectList(m.GenreGetAll(), "Id", "Name");

            return View(form);
        }

        // POST: Artist/Create
        [Authorize(Roles = "Executive")]
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(ArtistAdd newItem)
        {
            newItem.Executive = HttpContext.User.Identity.Name;

            // Validate the input
            if (!ModelState.IsValid)
            {
                return View(newItem);
            }

            // Process the input
            var addedItem = m.ArtistAdd(newItem);

            if (addedItem == null)
            {
                return View(newItem);
            }
            else
            {
                return RedirectToAction("Details", new { id = addedItem.Id });
            }
        }

        [Authorize(Roles = "Coordinator")]
        [Route("artist/{id}/addalbum")]
        // GET: Artist/Create
        public ActionResult AddAlbum(int? Id)
        {

            var a = m.ArtistGetByIdWithDetail(Id.GetValueOrDefault());

            var vals = new List<int> { a.Id };

            var form = new AlbumAddForm();
            form.GenreList = new SelectList(m.GenreGetAll(), "Id", "Name");

            form.ArtistName = a.Name;

            return View(form);
        }

        // POST: Artist/AddAblum
        [Route("artist/{id}/addalbum")]
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult AddAlbum(AlbumAdd newItem)
        {
            newItem.Coordinator = HttpContext.User.Identity.Name;

            // Validate the input
            if (!ModelState.IsValid)
            {
                return View(newItem);
            }

            // Process the input
            var addedItem = m.AlbumAdd(newItem);

            if (addedItem == null)
            {
                return View(newItem);
            }
            else
            {
                return RedirectToAction("../album/details", new { id = addedItem.Id });
            }
        }

        [Route("artist/{id}/addmediaitem")]
        [Authorize(Roles = "Executive")]
        public ActionResult AddMediaItem(int? id)
        {
            // Attempt to get the matching object
            var o = m.ArtistGetByIdWithDetail(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Create a form
                var form = new MediaItemAddForm();
                // Configure its property values
                form.ArtistId = o.Id;
                form.ArtistInfo = $"{o.Name}, {o.Genre}";

                // Pass the object to the view
                return View(form);
            }
        }

        [HttpPost]
        [Route("artist/{id}/AddMediaItem")]
        public ActionResult AddMediaItem(int? id, MediaItemAdd newItem)
        {
            // Validate the input
            // Two conditions must be checked
            if (!ModelState.IsValid && id.GetValueOrDefault() == newItem.ArtistId)
            {
                return View(newItem);
            }

            // Process the input
            var addedItem = m.ArtistMediaItemAdd(newItem);

            if (addedItem == null)
            {
                return View(newItem);
            }
            else
            {
                return RedirectToAction("details", new { id = addedItem.Id });
            }
        }

    }
}
