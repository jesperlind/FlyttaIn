using System.Collections.Generic;

namespace FlyttaIn.Services.Communications
{
    public class Communications
    {

        public List<Stop> GetStops(float latitude, float longitude)
        {
            var stops = new List<Stop>();
            stops.Add(new Stop {Name = "Spårvagn - Tjötgatan", TimeToWalkTo = "4 min", Distance = "400m" });
            stops.Add(new Stop {Name = "Spårvagn - Korsvägen", TimeToWalkTo = "4 min", Distance = "400m"});

            return stops;
            //return Json(new { SourceName = SourceName, Title = "Test", Content = "Some content" });
        }
    }

}