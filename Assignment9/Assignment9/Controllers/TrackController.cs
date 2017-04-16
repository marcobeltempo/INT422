using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment9.Controllers
{
    public class TrackController : Controller
    {
        Manager m = new Manager();
  
        //GET: Track
        public ActionResult Index()
        {
            return View(m.TrackGetAll());
        }

        //GET: Track/Details/
        public ActionResult Details(int? id)
        {
            var o = m.TrackGetByIdWithDetail(id.GetValueOrDefault());

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

        // GET: Track/Edit/
        [Authorize(Roles = "Clerk")]
        public ActionResult Edit(int? id)
        {
            var o = m.TrackGetByIdWithDetail(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                var form = Mapper.Map<TrackWithDetail, TrackEditForm>(o);

                return View(form);
            }
        }

        //POST: Track/Edit/
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(int? id, TrackEdit newItem)
        {
            newItem.Clerk = HttpContext.User.Identity.Name;
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Edit", new { id = newItem.Id });
            }

            if (id.GetValueOrDefault() != newItem.Id)
            {
                return RedirectToAction("Index");
            }
            // Process the input
            var addedItem = m.TrackEdit(newItem);

            addedItem.AudioUpload = newItem.AudioUpload;

            if (addedItem == null)
            {
                return View(newItem);
            }
            else
            {
                return RedirectToAction("../Track/Details", new { id = addedItem.Id });
            }
        }


        // GET: Track/Delete/
        [Authorize(Roles = "Clerk")]
        public ActionResult Delete(int? id)
        {
            var itemToDelete = m.TrackGetByIdWithDetail(id.GetValueOrDefault());

            if (itemToDelete == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(itemToDelete);
            }
        }

        // POST: Track/Delete/
        [HttpPost]
        public ActionResult Delete(int? id, FormCollection collection)
        {
            var result = m.TrackDelete(id.GetValueOrDefault());
            return RedirectToAction("index");
        }
    }
}
