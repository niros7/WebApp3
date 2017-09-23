using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Mvc;
using WebApplication.Models;
using WebApplication.ViewModels;

namespace WebApplication.Controllers
{
    public class BlogController : Controller
    {
        private readonly ApplicationDbContext dbContext = new ApplicationDbContext();

        public ActionResult Index()
        {

            var currentUserId = User.Identity.GetUserId();

            dbContext.Posts.Add(new Post
            {
                Author = dbContext.Users.Single(x => x.Id == currentUserId),
                Comments = null,
                CreationDate = DateTime.Now,
                Description = "LLLuuuuullllllllluuuuuuuuuuuuuuu",
                SubTitle = "wallackkk",
                Title = "Niros",
            });

            dbContext.SaveChanges();

            var postsTitles = dbContext.Posts.Select(post => new PostTitle
            {
                Title = post.Title,
                SubTitle = post.SubTitle,
                CreationDate = post.CreationDate,
                UserEmail = post.Author.Email

            });
            return View(postsTitles);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}