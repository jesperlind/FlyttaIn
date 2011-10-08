using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        /// http://localhost:5944/api/get/test
        /// </summary>
        /// <param name="sourceName"></param>
        /// <returns></returns>
        public JsonResult Get(string sourceName)
        {
            return Json(new { SourceName = sourceName, Title = "Test", Content = "Some content" }, JsonRequestBehavior.AllowGet);
        }

    }
}
