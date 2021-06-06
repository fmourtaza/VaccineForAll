using Newtonsoft.Json.Linq;
using RestSharp;
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
                    wc.Credentials = CredentialCache.DefaultCredentials;
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
                request.UseDefaultCredentials = true;
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

        public string GenerateOtp(String url, CitizenMobile citizenMobile)
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    wc.Headers[HttpRequestHeader.Accept] = "application/json";
                    wc.Headers[HttpRequestHeader.ContentType] = "application/json";
                    string Json = "{\"mobile\":\"9923800952\"}";
                    var result = wc.UploadString(url, Json);
                    return result;
                }

                //var client = new RestClient("https://cdn-api.co-vin.in/api/v2/auth/generateMobileOTP");
                //client.Timeout = -1;
                //var request = new RestRequest(Method.POST);
                //string Json = "{\"mobile\":\"9923800952\"}";
                //request.AddParameter("text/plain", "{\r\n    \"mobile\": \"9923800952\"\"\r\n}", ParameterType.RequestBody);
                //IRestResponse response = client.Execute(request);
                //Console.WriteLine(response.Content);
            }
            catch (Exception ex)
            {
                //Utilities.HandleException(ex);
            }

            return null;
        }

    }

}
