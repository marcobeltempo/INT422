using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment9.Controllers
{
    public class MediaItemController : Controller
    { 
        Manager m = new Manager();

        public ActionResult Index()
        {
            return View("Index", "home");
        }

        // GET: Photo/5
        [Route("MediaItem/{stringId}")]
        public ActionResult Details(string stringId = "")
        {

            var o = m.ArtistMediaItemGetById(stringId);

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                return File(o.Content, o.ContentType);
            }
        }

        [Route("mediaitem/{stringId}/download")]
        public ActionResult DetailsDownload(string stringId = "")
        {
            var o = m.ArtistMediaItemGetById(stringId);

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                string extension;
                RegistryKey key;
                object value;

                
                key = Registry.ClassesRoot.OpenSubKey(@"MIME\Database\Content Type\" + o.ContentType, false);
                
                value = (key == null) ? null : key.GetValue("Extension", null);
               
                extension = (value == null) ? string.Empty : value.ToString();

                var cd = new System.Net.Mime.ContentDisposition
                {
                    // Assemble the file name + extension
                    FileName = $"img-{stringId}{extension}",
                    // Force the media item to be saved (not viewed)
                    Inline = false
                };
                // Add the header to the response
                Response.AppendHeader("Content-Disposition", cd.ToString());

                return File(o.Content, o.ContentType);
            }
        }
    }
}
