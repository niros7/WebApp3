using System;

namespace WebApplication.ViewModels
{
    public class PostTitle
    {
        public int PostTitleId { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public DateTime CreationDate { get; set; }
        public string UserEmail { get; set; }
    }
}