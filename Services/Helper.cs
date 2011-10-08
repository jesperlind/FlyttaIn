using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;

namespace FlyttaIn.Services
{
    public static class Helper
    {
        public static string CreateHttpPost(string URI, string Parameters)
        {
            System.Net.WebRequest req = System.Net.WebRequest.Create(URI);
            //req.Proxy = new System.Net.WebProxy(ProxyString, true);
            //Add these, as we're doing a POST
            req.ContentType = "application/x-www-form-urlencoded";
            req.Method = "POST";
            //We need to count how many bytes we're sending. Post'ed Faked Forms should be name=value&
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(Parameters);
            req.ContentLength = bytes.Length;
            System.IO.Stream os = req.GetRequestStream();
            os.Write(bytes, 0, bytes.Length); //Push it out there
            os.Close();
            System.Net.WebResponse resp = req.GetResponse();
            if (resp == null) return null;
            System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
            return sr.ReadToEnd().Trim();
        }

        public static string CreateHttpGet(string URI,ContentType contentType)
        {
            System.Net.WebRequest req = System.Net.WebRequest.Create(URI);
            
            req.ContentType = GetContentType(contentType);
            req.Method = "GET";
            System.Net.WebResponse resp = req.GetResponse();
            if (resp == null) return null;
            System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
            return sr.ReadToEnd().Trim();
        }

        private static string GetContentType(ContentType contentType)
        {
            
            if (contentType == ContentType.Json)
            {
                return "application/json";
            }
            return "application/x-www-form-urlencoded";

        }

        public enum ContentType
        {
            UrlEncoded,
            Json
        }

        /// <summary>
        /// 
        /*
         * StopLocation":[{
            "name":"Nordstan, Göteborg",
            "lon":"11.970791",
            "lat":"57.709245",
            "id":"9021014004945000"
            },{
            "name":"Nordskagen, Götene",
            "lon":"13.431475",
            "lat":"58.594953",
            "id":"9021014034152000"
            },  etc..
         * */
        /// </summary>
        /// <param name="url"></param>
        /// <param name="api"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public static IList<JToken> GetGBGLocationToCoord(string url, string api, string location)
        {
            string newUrl = string.Format(url, api, location);
            var res = CreateHttpGet(newUrl, ContentType.Json);
            res = res.Replace("processJSON(", "");
            res = res.Replace(");", "");
            
            JObject googleSearch = JObject.Parse(res);

            // get JSON result objects into a list
            IList<JToken> results = googleSearch["LocationList"]["StopLocation"].Children().ToList();

            return results;
        }
    }
}