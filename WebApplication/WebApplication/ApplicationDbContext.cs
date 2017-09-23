using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using WebApplication.Models;

namespace WebApplication
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("DefaultConnection", throwIfV1Schema: false) { }

        public DbSet<Comment> Commnets { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }


    //public class DbContextSeed : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    //{
    //}
}