using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment9.Controllers
{

    public class PhotoController : Controller
    {
        Manager m = new Manager();

        public ActionResult Index()
        {
            return View("Index", "Home");
        }

        [Route("clip/{id}")]
        public ActionResult Details(int? id)
        {
            var o = m.TrackAudioGetById(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                return File(o.Audio, o.AudioContentType);
            }
        }
    }
}