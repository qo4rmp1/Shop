using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECApplication.Models
{
    public class Area
    {
        public string zip { get; set; }
        public string text { get; set; }
    }

    public class Taiwan
    {
        public string city { get; set; }
        public List<Area> area { get; set; }
    }

    public class TwCity
    {
        public List<Taiwan> taiwan { get; set; }
    }
}