using log4net;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace IOT_Device.RestFull
{
    public class RestApi
    {
        private readonly ILog log = LogManager.GetLogger(typeof(RestApi));
        private static RestApi m_instance;

        private RestApi()
        {

        }

        /// <summary>
        /// Get the singleton instance
        /// </summary>
        /// <returns>The singleton instance</returns>
        public static RestApi Instance()
        {
            if (m_instance == null)
            {
                m_instance = new RestApi();
            }
            return m_instance;
        }

        public string POST(JObject json, string url)
        {
            log.Debug($"Start POST to URL: {url} with json: {json}");
            //log start POST
            string result;
            try
            {
                Uri myUri = new Uri(url);
                WebRequest myWebRequest = HttpWebRequest.Create(myUri);
                HttpWebRequest httpWebRequest = (HttpWebRequest)myWebRequest;
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                httpWebRequest.PreAuthenticate = true;
                httpWebRequest.AuthenticationLevel = System.Net.Security.AuthenticationLevel.MutualAuthRequested;
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(json.ToString());
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                    log.Debug($"Result: {result}");
                }
            }
            catch (Exception ex)
            {
                result = $"Error message: {ex.Message}";
                log.Error(result);
            }
            return result;
        }

        
        public string GET(string url)
        {
            log.Debug($"Start GET to URL: {url} ");
            //log start POST
            string result;
            try
            {
                //_AWSUrl += _days;
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "GET";
                httpWebRequest.PreAuthenticate = true;
                httpWebRequest.AuthenticationLevel = System.Net.Security.AuthenticationLevel.MutualAuthRequested;


                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                    log.Debug($"Result: {result}");
                }
            }
            catch (Exception ex)
            {
                result = $"Error message: {ex.Message}";
                log.Error(result);
            }
            return result;
        }
        
    }
}
