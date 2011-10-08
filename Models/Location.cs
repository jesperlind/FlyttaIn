using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlyttaIn.Models
{
    /// <summary>
    /// save responses from the vasttrafik api in this format, it will make calls to trip service easier
    /// </summary>
    public class Location
    {
        public string Name;
        public string Identifier;
        public string Lat;
        public string Long;
    }
}