using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment9.Controllers
{
    public class AlbumController : Controller
    {
        Manager m = new Manager();

        // GET: Album
        public ActionResult Index()
        {
            return View(m.AlbumGetAll());
        }

        // GET: Album/Details/     
        public ActionResult Details(int? id)
        {
            // Attempt to get the matching object
            var o = m.AlbumGetByIdWithDetail(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(o);
            }
        }

        // GET: Album/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // GET: Track/Create
        [Authorize(Roles = "Clerk")]
        [Route("album/{id}/addtrack")]
        public ActionResult AddTrack()
        {
            var form = new TrackAddForm();
            form.GenreList = new SelectList(m.GenreGetAll(), "Id", "Name");

            return View(form);
        }

        // POST: Track/Create
        [Route("album/{id}/addtrack")]
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult AddTrack(TrackAdd newItem)
        {
            newItem.Clerk = HttpContext.User.Identity.Name;

            // Validate the input
            if (!ModelState.IsValid)
            {
                return View(newItem);
            }

            // Process the input
            var addedItem = m.TrackAdd(newItem);

            if (addedItem == null)
            {
                return View(newItem);
            }
            else
            {
                return RedirectToAction("../Track/Details", new { id = addedItem.Id });
            }
        }
    }
}