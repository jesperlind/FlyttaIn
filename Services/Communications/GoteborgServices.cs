using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlyttaIn.Services;
using Newtonsoft.Json.Linq;
using FlyttaIn.Models;

namespace FlyttaIn.Services.Communications
{
    public class GoteborgServices
    {

        private const string API_KEY = "69aeda59-f7a9-4667-81fb-c6ad913f8eb9";

        public const string VASTTRAFIK_NAME_TO_COORD = "http://api.vasttrafik.se/bin/rest.exe/v1/location.name?authKey={0}&format=json&jsonpCallback=processJSON&input={1}";
        public const string VASTTRAFIK_NAME_TO_COORD_NEARBY = "http://api.vasttrafik.se/bin/rest.exe/v1/location.nearbystops?authKey={0}&format=json&jsonpCallback=processJSON&originCoordLat={1}&originCoordLong={2}&maxNo={3}";

        public const string VASTTRAFIK_TRIP = "http://api.vasttrafik.se/bin/rest.exe/v1/trip?authKey={0}&format=json&jsonpCallback=processJSON&date={1}&time={2}&originId={3}&originCoordLat={4}&originCoordLong={5}&destId={6}&destCoordLat={7}&destCoordLong={8}";
        public const string VASTTRAFIK_API_KEY = "e39042b8-db1a-46a9-ba81-2ae8893aa393";


        /// <summary>
        /// call function GetGBGLocationToCoord in this class to get identifier and coordinates
        /// gives function a static time for the time being, reconsider changing
        /// </summary>
        /// <param name="url"></param>
        /// <param name="key"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public string GetGBGTrip(string url, string key, Location from, Location to)
        {
            var newUrl = string.Format(url
                , key
                , DateTime.Now.AddDays(1).ToShortDateString()
                , "10:30"
                , from.Identifier
                , from.Lat
                , from.Long
                , to.Identifier
                , to.Lat
                , to.Long

                );
            var res = Helper.CreateHttpGet(newUrl, Helper.ContentType.Json);
            return "";
        }


        public IList<JToken> GetGBGLocationToCoord(string location)
        {
            string newUrl = string.Format(VASTTRAFIK_NAME_TO_COORD, VASTTRAFIK_API_KEY, location);
            var res = Helper.CreateHttpGet(newUrl, Helper.ContentType.Json);
            res = res.Replace("processJSON(", "");
            res = res.Replace(");", "");

            JObject googleSearch = JObject.Parse(res);

            // get JSON result objects into a list
            IList<JToken> results = googleSearch["LocationList"]["StopLocation"].Children().ToList();



            return results;
        }

        public static IList<JToken> GetGBGLocationToNearbyCoord(string url, string api, string originLat, string originLong, int maxNoRows = 10)
        {
            var newUrl = string.Format(url, api, originLat, originLong, maxNoRows.ToString());
            var res = Helper.CreateHttpGet(newUrl, Helper.ContentType.Json);

            //clean response
            res = res.Replace("processJSON(", "");
            res = res.Replace(");", "");
            JObject googleSearch = JObject.Parse(res);
            // get JSON result objects into a list
            IList<JToken> results = googleSearch["LocationList"]["StopLocation"].Children().ToList();
            return results;
        }

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