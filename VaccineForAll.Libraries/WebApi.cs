using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace VaccineForAll.Libraries
{
    public class WebApi
    {
        public string Get(String url)
        {
            try
            {
                using (WebClient wc = new WebClient())
                {

                    wc.Headers[HttpRequestHeader.ContentType] = "application/json";
                    wc.Headers[HttpRequestHeader.AcceptLanguage] = "hi_IN";
                    wc.Headers[HttpRequestHeader.UserAgent] = " Mozilla/5.0 (Windows NT 6.1; WOW64; rv:25.0) Gecko/20100101 Firefox/25.0";
                    return wc.DownloadString(url);
                }
            }
            catch (WebException wex)
            {
                if (((HttpWebResponse)wex.Response).StatusCode == HttpStatusCode.NotFound)
                {
                    Utilities.HandleException(wex);
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
