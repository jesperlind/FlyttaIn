using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlyttaIn.Services;
using FlyttaIn.Services.Communications;

namespace FlyttaIn.Controllers
{
    public class ApiController : Controller
    {
        //
        // GET: /Api/

        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// http://localhost:5944/api/communications?latitude=0.2&longitude=1.2
        /// </summary>
        /// <returns></returns>
        public JsonResult Communications(float latitude, float longitude)
        {

            var stops = new Communications().GetStops(latitude, longitude);

            return Json(stops, JsonRequestBehavior.AllowGet);
            
            //return Json(new { SourceName = sourceName, Title = "Test", Content = "Some content" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MyLocal()
        {
            var s = new GoteborgServices();
            var test = s.GetGBGLocationToCoord("Tredje Långgatan 13B");

            //var stops = new Communications().GetStops(latitude, longitude);

            //return Json(stops, JsonRequestBehavior.AllowGet);
            
            return Json(new { Title = "Test", Content = "Some content" }, JsonRequestBehavior.AllowGet);
        }

        

    }
}
