using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace FlyttaIn.Services.Carpool
{
    public class Carpool
    {
        public Dictionary<string, string> getNearest(float lat, float lon)
	{
	    XmlDocument doc = new XmlDocument();
	    doc.Load("App_Data/bilpooler.xml");

	    XmlNodeList nodeList;
            XmlNode root = doc.DocumentElement;
	    
            float maxDistance = 0.0f;

	    string name;

            foreach(var pool in root.ChildNodes)
            {
		var poolList = pool.Cast<XmlNode>().ToList();
		var poolName = list.Where(x => x.Name == "name").Single();
		var poolLat = list.Where(x => x.Name == "lat").Single();
		var poolLon = list.Where(x => x.Name == "lon").Single();

		var latDist = Math.Abs(poolLat - lat);
		var lonDist = Math.Abs(poolLon - lon);

                double R = 6371;
 
            	double dLat = this.toRadian(poolLat - lat);
            	double dLon = this.toRadian(poolLon - lon);
 
            	double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Cos(this.toRadian(pos1.Latitude)) *Math.Cos(this.toRadian(pos2.Latitude)) * Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            	double c = 2 * Math.Asin(Math.Min(1, Math.Sqrt(a)));
            	double d = R * c;

		if (d > maxDistance) {
		    maxDistance = d;
		    name = poolName;
		}
	    }

	    Dictionary<string,string> dict = new Dictionary<string,string>();	    

	    dict.Add("distance", maxDistance.ToString());
	    dict.Add("name", name);

	    return d;
	}
    }
}
