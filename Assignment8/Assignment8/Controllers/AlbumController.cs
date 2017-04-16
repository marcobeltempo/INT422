using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace Assignment8.Controllers
{
    public class AlbumController : Controller
    {

        Manager m = new Manager();

        // GET: Album
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(m.AlbumGetAll());
        }

        

        [Authorize(Roles = "Clerk")]
        [Route("album/{id}/addtrack")]
        // GET: Track/Create
        public ActionResult AddTrack()
        {
            var form = new TrackAddForm();
            form.GenreList = new SelectList(m.GenreGetAll(), "Id", "Name");

            return View(form);
        }

        [HttpPost]
        [Authorize(Roles = "Clerk")]
        [Route("album/{id}/addtrack")]   
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

        // GET: Album/Details/5
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
                // Pass the object to the view
                return View(o);
            }
        }

        // GET: Album/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Album/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Album/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Album/Delete/5
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