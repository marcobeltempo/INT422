using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment8.Controllers
{
    public class ArtistController : Controller
    {
      private Manager m = new Manager();

        // GET: Artist
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(m.ArtistGetAll());
        }

        // GET: Artist/Details/5
        public ActionResult Details(int? id)
        {
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
                return RedirectToAction("details", new { id = addedItem.Id });
            }
        }

        // GET: Artist/Create
        [Authorize(Roles = "Coordinator")]
        [Route("artist/{id}/addalbum")] 
        public ActionResult AddAlbum(int? Id)
        {
            var form = new AlbumAddForm();

            var a = m.ArtistGetByIdWithDetail(Id.GetValueOrDefault());

            var values = new List<int> { a.Id };

            form.KnownArtists = a.Name;

            form.GenreList = new SelectList
                ( m.GenreGetAll(), "Id", "Name");

            form.ArtistList = new MultiSelectList
                ( items: m.ArtistGetAll(),
                dataValueField: "Id",
                dataTextField: "Name", 
                selectedValues: values);

            form.TrackList = new MultiSelectList
                ( items: m.TrackGetAll(),
                dataValueField: "Id",
                dataTextField: "Name");
                

            return View(form);
        }

        // POST: Artist/Create
        [HttpPost]
        [Authorize(Roles = "Coordinator")]
        [Route("artist/{id}/addalbum")]
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
        // POST: Artist/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Artist/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Artist/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
