using System.Collections.Generic;

namespace WebApplication.ViewModels
{
    public class CreatePost
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}