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
    public class TraficInformationService
    {

        private const string YRNO_URL = @"http://www.yr.no/place/{0}/{1}/{1}/forecast.xml";
        private const string VASTTRAFIK_URL_ALL_VALID_ITEMS = @"http://vasttrafik.se/external_services/TrafficInformation.asmx/GetAllValidItems";
        private const string VASTTRAFIK_INFORMATION_WITH_GEOGRAPHY = @"http://vasttrafik.se/external_services/TrafficInformation.asmx/GetTrafficInformationWithGeography";
        public const string VASTTRAFIK_NAME_TO_COORD_NEARBY = "http://api.vasttrafik.se/bin/rest.exe/v1/location.nearbystops?authKey={0}&format=json&jsonpCallback=processJSON&originCoordLat={1}&originCoordLong={2}&maxNo={3}";
        public const string VASTTRAFIK_NAME_TO_COORD = "http://api.vasttrafik.se/bin/rest.exe/v1/location.name?authKey={0}&format=json&jsonpCallback=processJSON&input={1}";
        private const string MEASURE_STATIONS_DATA_GBG = "http://data.goteborg.se/waterflowservice/GetMeasureStations/{0}?latitude={1}&longitude={2}&format={3}";
        private const string TRAFIKLAB_SEARCH = "https://api.trafiklab.se/samtrafiken/resrobot/Search.xml?key=[YOUR API KEY]&from=Mörrum+gatan+1&to=Stockholm&fromX=14.749874&fromY=56.193701&toX=18.058405&toY=59.329938&coordSys=WGS84&a";

        public List<TrafficInformation> GetVasttrafik(string station, string url)
        {
            return GetTrafficInformation(Helper.CreateHttpPost(url, string.Format("identifier={0}", station)));
        }

        public string GetGBGData(string appid, string latitude, string longitude, string format)
        {

            return Helper.CreateHttpGet(String.Format(MEASURE_STATIONS_DATA_GBG, appid, latitude, longitude, format),Helper.ContentType.UrlEncoded);
        }

        public List<TrafficInformation> GetTrafficInformation(string xml)
        {

            List<TrafficInformation> retval = new List<TrafficInformation>();

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            var root = doc.DocumentElement;
            foreach (XmlNode trafficInformation in root.ChildNodes)
            {
                var trafficObject = trafficInformation.ChildNodes
                    .Cast<XmlNode>().ToList();

                var PageName =
                    trafficObject.Where(x => x.Name == "PageName")
                    .Single();

                var Area = trafficObject
                    .Where(x => x.Name == "Area")
                    .Single();

                var MainBody = trafficObject
                    .Where(x => x.Name == "MainBody")
                    .Single();

                var Municipality = trafficObject
                    .Where(x => x.Name == "Municipality")
                    .Single();

                var DateFrom = trafficObject
                    .Where(x => x.Name == "DateFrom")
                    .Single();

                var DateTo = trafficObject
                    .Where(x => x.Name == "DateTo")
                    .Single();

                var tf = new TrafficInformation { PageName = PageName.InnerText, Area = Area.InnerText, MainBody = MainBody.InnerText, Municipality = Municipality.InnerText, DateFrom = DateTime.Parse(DateFrom.InnerText), DateTo = DateTime.Parse(DateTo.InnerText) };
                retval.Add(tf);
            }
            return retval;

        }
    }
}