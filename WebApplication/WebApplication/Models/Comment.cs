namespace WebApplication.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public ApplicationUser CommentUser { get; set; }
        public Post ReferencedPost { get; set; }
    }
}