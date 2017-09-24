using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class StatisticsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        const int GraphGroupNumber = 5;
        public static int[] RANGES = { 1, 3, 5, 7, 10 };
        public static string[] Colors = {"#3366CC","#DC3912", "#FF9900" , "#109618" , "#990099" };

        // Get /Statistics/Graphs
        public ActionResult Graphs()
        {
            ViewBag.title = "get this title from controller";

            return View();
        }

        public ActionResult GroupByTagsGraph()
        {
            var groupby = db.Posts.GroupBy(g => g.PostTags).ToArray();
            return Json(groupby, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CountCommentsOfPostsGraph()
        {
            // graph counting comments of posts diveded to ranges
            int[] RangesArrayConunt = new int[5];

            var posts = db.Posts.Select(p => p.Comments.Count).ToArray();
            foreach (var item in posts)
            {
                if (item <= RANGES[0])
                {
                    RangesArrayConunt[0]++;
                }
                else if (item <= RANGES[1] && item > RANGES[0])
                {
                    RangesArrayConunt[1]++;
                }
                else if (item <= RANGES[2] && item > RANGES[1])
                {
                    RangesArrayConunt[2]++;
                }
                else if (item <= RANGES[3] && item > RANGES[2])
                {
                    RangesArrayConunt[3]++;
                }
                else if (item <= RANGES[4] && item > RANGES[3])
                {
                    RangesArrayConunt[4]++;
                }
            }

            List<Models.GraphGroup> l = new List<Models.GraphGroup>();
            //TODO : add the ranges lables the first and last 
            for (int i = 0; i < GraphGroupNumber; i++)
            {
                GraphGroup groupToAdd = new GraphGroup(RANGES[i].ToString(), RangesArrayConunt[i].ToString(), Colors[i]);
                l.Add(groupToAdd);
            }

            return Json(l, JsonRequestBehavior.AllowGet);

            /*
            return Json(new[] {
        new { label = "down3" ,value = "50", color = "#3366CC" },
        new { label = "3-5",value="1000" , color = "#DC3912" },
        new { label = "5-7" ,value = "250", color = "#FF9900" },
        new { label = "7-10" , value="700", color = "#109618" },
        new { label = "10up" ,value = "900", color = "#990099" }
    }, JsonRequestBehavior.AllowGet);

    */

        }
    }
}