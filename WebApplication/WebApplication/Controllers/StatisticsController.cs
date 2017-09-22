using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class StatisticsController : Controller
    {
        // Get /Statistics/Graphs
        public ActionResult Graphs()
        {
            ViewBag.title = "get this title from controller";

            return View();
        }


        public ActionResult MyAction()
        {
            return Json(new[] {
        new { label = "Basic" ,value = "50", color = "#3366CC" },
        new { label = "Plus",value="1000" , color = "#DC3912" },
        new { label = "Lite" ,value = "250", color = "#FF9900" },
        new { label = "Elite" , value="700", color = "#109618" },
        new { label = "Delux" ,value = "900", color = "#990099" }
    }, JsonRequestBehavior.AllowGet);
        }
    }
}