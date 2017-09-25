using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebApplication.Models;
using WebApplication.ViewModels;

namespace WebApplication.Controllers
{
    [RoutePrefix("posts")]
    public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Posts
        public ActionResult Index()
        {
            List<PostTitle> postsToReturn = new List<PostTitle>();

            if (User.Identity.IsAuthenticated)
            {
                var currentUserId = User.Identity.GetUserId();

                var currentUserTags = db.Posts.Include(path => path.PostTags).Include(p => p.Author)
                    .Where(p => p.Author.Id == currentUserId).Select(p => p.PostTags).SelectMany(x => x);

                var bestPosts = db.Posts.Include(post => post.PostTags).Select(p => new
                {
                    postId = p.Id,
                    matchCount = currentUserTags.Intersect(p.PostTags).Count(),
                    creationDate = p.CreationDate
                })
                .OrderByDescending(x => x.matchCount).ThenByDescending(x => x.creationDate).ToList();

                foreach (var item in bestPosts)
                {
                    postsToReturn.AddRange(db.Posts.Where(p => p.Id == item.postId).Select(post => new PostTitle
                    {
                        Title = post.Title,
                        SubTitle = post.SubTitle,
                        CreationDate = post.CreationDate,
                        UserEmail = post.Author.Email,
                        PostTitleId = post.Id

                    }).ToList());
                }
            }
            else
            {
                postsToReturn = db.Posts.OrderByDescending(p => p.CreationDate).Select(post => new PostTitle
                {
                    Title = post.Title,
                    SubTitle = post.SubTitle,
                    CreationDate = post.CreationDate,
                    UserEmail = post.Author.Email,
                    PostTitleId = post.Id

                }).ToList();
            }

            return View(postsToReturn);
        }

        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Include(p => p.Author).Where(p => p.Id == id.Value).First();
            post.Comments = db.Commnets.Where(c => c.ReferencedPost.Id == id.Value).Include(c => c.CommentUser).ToList();

            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }


        [HttpGet]
        [Route("search")]
        public ActionResult Search(SearchPost searchParams)
        {
            List<PostTitle> postsToReturn = new List<PostTitle>();

            if (string.IsNullOrWhiteSpace(searchParams.Username) &&
                string.IsNullOrWhiteSpace(searchParams.SubTitle) &&
                string.IsNullOrWhiteSpace(searchParams.Title))
            {
                postsToReturn.AddRange(db.Posts.OrderByDescending(p => p.CreationDate).Select(post => new PostTitle
                {
                    Title = post.Title,
                    SubTitle = post.SubTitle,
                    CreationDate = post.CreationDate,
                    UserEmail = post.Author.Email,
                    PostTitleId = post.Id

                }).ToList());
            }
            else
            {
                Func<Post, bool> usernamePredicate = (Post p) => { return true; };
                Func<Post, bool> titlePredicate = (Post p) => { return true; };
                Func<Post, bool> subTitlePredicate = (Post p) => { return true; };

                if (!string.IsNullOrWhiteSpace(searchParams.Username))
                {
                    usernamePredicate = (Post p) => p.Author.Email.Contains(searchParams.Username);
                }

                if (!string.IsNullOrWhiteSpace(searchParams.Title))
                {
                    titlePredicate = (Post p) => p.Title.Contains(searchParams.Title);
                }

                if (!string.IsNullOrWhiteSpace(searchParams.SubTitle))
                {
                    subTitlePredicate = (Post p) => p.SubTitle.Contains(searchParams.SubTitle);
                }


                postsToReturn.AddRange(db.Posts.Include(p => p.Author)
                    .Where(usernamePredicate)
                    .Where(titlePredicate)
                    .Where(subTitlePredicate)
                    .OrderByDescending(p => p.CreationDate).Select(post => new PostTitle
                    {
                        Title = post.Title,
                        SubTitle = post.SubTitle,
                        CreationDate = post.CreationDate,
                        UserEmail = post.Author.Email,
                        PostTitleId = post.Id

                    }).ToList());
            }

            return View("Index", postsToReturn);
        }

        // GET: Posts/Create
        [Route("Create")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("{id}/comment")]
        public ActionResult CreateComment(int id, string commentDescription)
        {
            var post = db.Posts.Find(id);
            var currentUser = db.Users.Find(User.Identity.GetUserId());

            post.Comments.Add(new Comment
            {
                CommentUser = currentUser,
                Description = commentDescription,
                ReferencedPost = post
            });

            if (db.SaveChanges() == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Create")]
        public ActionResult Create(CreatePost createPost)
        {
            var post = new Post();
            if (ModelState.IsValid)
            {
                post.PostTags = createPost.Tags.Select(x => new Tag { TagTitle = x }).ToList();
                post.Title = createPost.Title;
                post.SubTitle = createPost.SubTitle;
                post.Description = createPost.Description;
                post.CreationDate = DateTime.Now;
                post.Author = db.Users.Find(User.Identity.GetUserId());
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Index");
        }

        // GET: Posts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,SubTitle,SiteUrl,Description,CreationDate")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult Delete(int id)
        {
            Post post = db.Posts.Include(p => p.Comments).Where(p => p.Id == id).Single();
            db.Commnets.RemoveRange(post.Comments);
            db.Tags.RemoveRange(post.PostTags);
            db.Posts.Remove(post);
            db.SaveChanges();

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
