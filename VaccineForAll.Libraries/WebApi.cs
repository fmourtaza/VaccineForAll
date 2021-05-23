using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace VaccineForAll.Libraries
{
    public class WebApi
    {
        public string GetUsingWebClient(String url)
        {
            try
            {
                using (WebClient wc = new WebClient())
                {

                    wc.Headers[HttpRequestHeader.ContentType] = "application/json";
                    wc.Headers[HttpRequestHeader.AcceptLanguage] = "hi_IN";
                    wc.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:25.0) Gecko/20100101 Firefox/25.0";
                    wc.Headers[HttpRequestHeader.KeepAlive] = "true";
                    wc.Headers.Add("Access-Control-Allow-Origin", "http://*.vaccineforall.co.in");
                    return wc.DownloadString(url);
                }
            }
            catch (WebException wex)
            {
                if (((HttpWebResponse)wex.Response).StatusCode == HttpStatusCode.NotFound)
                {
                }
                //Fall back mechanism
                return GetUsingHttp(url);
            }
            catch (Exception)
            {
                //Fall back mechanism
                return GetUsingHttp(url);
            }
        }

        public string GetUsingHttp(String url)
        {
            try
            {

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = "application/json";
                request.Headers.Add("Accept-Language", "hi_IN");
                request.Headers.Add("UserAgent", "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:25.0) Gecko/20100101 Firefox/25.0");
                request.Headers.Add("Access-Control-Allow-Origin", "http://*.vaccineforall.co.in");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusDescription == "OK")
                {
                    string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    return responseString;
                }
            }
            catch (Exception ex)
            {
                Utilities.HandleException(ex);
            }

            return null;
        }
    }
}
