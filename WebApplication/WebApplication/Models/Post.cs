using System;
using System.Collections.Generic;

namespace WebApplication.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string SiteUrl { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }

        public ApplicationUser Author { get; set; }
        public ICollection<Tag> PostTags { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}