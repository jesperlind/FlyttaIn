using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Linq;

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
        
            double maxDistance = 0.0f;

        string name = "";

            foreach(XmlNode pool in root.ChildNodes)
            {
        var poolList = pool.Cast<XmlNode>().ToList();
        var poolName = poolList.Where(x => x.Name == "name").Single().Value;
        var poolLat = Convert.ToDouble(poolList.Where(x => x.Name == "lat").Single().Value);
        var poolLon = Convert.ToDouble(poolList.Where(x => x.Name == "lon").Single().Value);

        var latDist = Math.Abs(poolLat - lat);
        var lonDist = Math.Abs(poolLon - lon);

                double R = 6371;
 
                double dLat = (Math.PI / 180) * (poolLat - lat);
                double dLon =  (Math.PI / 180) * (poolLon - lon);
 
                double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Cos( (Math.PI / 180) * (poolLat)) *Math.Cos( (Math.PI / 180) * (lat)) * Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
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

        return dict;
    }
    }
}
