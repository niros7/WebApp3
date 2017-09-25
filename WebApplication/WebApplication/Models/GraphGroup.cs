using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    
    public class GraphGroup
    {
        public GraphGroup(string Lable,string Value,string Color)
        {
            lable = Lable;
            value = Value;
            color = Color;

        }
        public string lable { get; set; }
        public string value { get; set; }
        public string color { get; set; }
    }
}