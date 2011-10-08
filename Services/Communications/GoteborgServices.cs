using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlyttaIn.Services;

namespace FlyttaIn.Services.Communications
{
    public class GoteborgServices
    {

        private const string API_KEY = "69aeda59-f7a9-4667-81fb-c6ad913f8eb9";
        
        public string GetWeather()
        {

            DateTime start = DateTime.Now.AddDays(-2d);
            DateTime end = DateTime.Now;

            //string url = "http://data.goteborg.se/AirQualityService/v1.0/Measurements/{" + API_KEY + "}?startdate={" + start.ToShortDateString() + "}&enddate={" + end.ToShortDateString() + "}&format={Json}";
            string url = "http://data.goteborg.se/AirQualityService/v1.0/LatestMeasurement/{" + API_KEY + "}?&format={Json}";

            return Helper.CreateHttpGet(url,Helper.ContentType.Json);
        }
    }
}