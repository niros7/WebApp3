using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.ViewModels;

namespace WebApplication.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin
        public ActionResult Index()
        {
            var postsToReturn = db.Posts.OrderByDescending(p => p.CreationDate).Select(post => new PostTitle
            {
                Title = post.Title,
                SubTitle = post.SubTitle,
                CreationDate = post.CreationDate,
                UserEmail = post.Author.Email,
                PostTitleId = post.Id

            }).ToList();

            return View(postsToReturn);
        }
    }
}
