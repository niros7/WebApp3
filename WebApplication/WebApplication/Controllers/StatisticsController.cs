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
        //public static string[] Colors = {"#3366CC","#DC3912", "#FF9900" , "#109618" , "#990099" };

        // Get /Statistics/Graphs
        public ActionResult Graphs()
        {
            ViewBag.title = "Statistics Page";

            return View();
        }

        public ActionResult GroupByTagsGraph()
        {
            var groupby = db.Posts.SelectMany(g => g.PostTags)
                .GroupBy(g => g.TagTitle)
                .Select(n => new { lable = n.Key, value = n.Count() ,color = ""})
                .ToList();
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

            GraphGroup firstNode = new GraphGroup("0 - " + RANGES[0].ToString(), RangesArrayConunt[0].ToString(), "");
            l.Add(firstNode);
            for (int i = 1; i < GraphGroupNumber-1; i++)
            {
                GraphGroup groupToAdd = new GraphGroup(RANGES[i-1].ToString() +" - "+RANGES[i].ToString(), RangesArrayConunt[i].ToString(), "");
                l.Add(groupToAdd);
            }
            GraphGroup lastNode = new GraphGroup(RANGES[GraphGroupNumber-1].ToString() + "+", RangesArrayConunt[GraphGroupNumber - 1].ToString(), "");
            return Json(l, JsonRequestBehavior.AllowGet);
        }
    }
}