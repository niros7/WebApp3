using System.Collections.Generic;

namespace WebApplication.ViewModels
{
    public class CreatePost
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public double? Lat { get; set; }
        public double? Lng { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}