using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
    }
}