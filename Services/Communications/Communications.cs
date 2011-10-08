using System.Collections.Generic;
using FlyttaIn.Services;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System;
using System.Linq;
using FlyttaIn.Models;

namespace FlyttaIn.Services.Communications
{
    public class Communications
    {

        public List<Stop> GetStops(float latitude, float longitude)
        {
            return new List<Stop>();
        }

        public List<Stop> GetStops2(float latitude, float longitude)
        {
            var stops = new List<Stop>();
            stops.Add(new Stop {Name = "Spårvagn - Tjötgatan", TimeToWalkTo = "4 min", Distance = "400m" });
            stops.Add(new Stop {Name = "Spårvagn - Korsvägen", TimeToWalkTo = "4 min", Distance = "400m"});

            return stops;
            //return Json(new { SourceName = SourceName, Title = "Test", Content = "Some content" });
        }

        
    }

}