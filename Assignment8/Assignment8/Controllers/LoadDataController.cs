using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment8.Controllers
{
    [Authorize(Roles = "Admin")]
    public class LoadDataController : Controller
    {
        // Reference to the manager object
        Manager m = new Manager();

        // GET: LoadData
        public ActionResult Index()
        {
            return View();
        }

        // GET: LoadData/RoleClaim
        public ActionResult RoleClaim()
        {
            ViewBag.Result = m.LoadDataRoleClaim() ? "Role Claim data was loaded" : "No Action: RoleClaim data already loaded";
            return View("result");
        }
        // GET: LoadData/Artist
        public ActionResult Artist()
        {
            ViewBag.Result = m.LoadDataArtist() ? "Artist data was loaded" : "No Action: Artist data already loaded";
            return View("result");
        }
        // GET: LoadData/Track
        public ActionResult Track()
        {
            ViewBag.Result = m.LoadDataTrack() ? "Track data was loaded" : "No Action: Track data already loaded";
            return View("result");
        }
        // GET: LoadData/Album
        public ActionResult Album()
        {
            ViewBag.Result = m.LoadDataAlbum() ? "Album data was loaded" : "No Action: Album data already loaded";
            return View("result");
        }

        // GET: LoadData/Genre
        public ActionResult Genre()
        {
            ViewBag.Result = m.LoadDataGenre() ? "Genre data was loaded" : "No Action: Genre data already loaded";
            return View("result");
        }

        public ActionResult Remove()
        {
            if (m.RemoveData())
            {
                return Content("data has been removed");
            }
            else
            {
                return Content("could not remove data");
            }
        }

        public ActionResult RemoveDatabase()
        {
            if (m.RemoveDatabase())
            {
                return Content("database has been removed");
            }
            else
            {
                return Content("could not remove database");
            }
        }

    }
}