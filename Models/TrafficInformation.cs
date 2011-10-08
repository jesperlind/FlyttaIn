using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlyttaIn.Models
{
    public class TrafficInformation
    {
        public string PageName { get; set; }
        public string Area { get; set; }
        public string Municipality { get; set; }
        public int Line { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int Priority { get; set; }
        public string MainBody { get; set; }

    }
}
