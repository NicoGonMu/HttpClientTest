using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PruebaHttpClient.Services
{
    public class Services : IServices
    {
        private const string PETITION = "/v1/fraud/protect/manage/organizations/test_back/fields";
        public static HttpClient _client;

        public Services(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<string>[]> sendQueryAsync(string userName)
        {
            List<string>[] ret = new List<string>[4];
            ret[0] = new List<string>(); ret[1] = new List<string>(); ret[2] = new List<string>(); ret[3] = new List<string>();

            Dictionary<string, string> headers = new Dictionary<string, string>() { { "Accept", "application/json" }, { "Authorization", "**********" } };
            NetworkCredential _credentials = new NetworkCredential(userName, "**********");
           
            for (int i = 0; i < 20; i++)
            {
                ret[0].Add("https://fraud-protect-manage.swappt.com" + DoGetAsync("https://fraud-protect-manage.swappt.com" + PETITION, "", headers, 1000, _credentials));
                ret[1].Add("http://localhost:8080" + DoGetAsync("http://localhost:8080" + PETITION, "", headers, 5000, _credentials));
                ret[2].Add("http://localhost:8000" + DoGetAsync("http://localhost:8000" + PETITION, "", headers, 2000, _credentials));
                ret[3].Add("http://localhost:8005" + DoGetAsync("http://localhost:8005" + PETITION, "", headers, 100, _credentials));
            }
            return ret;
        }

        #region httpconnection
        public static async Task<string> DoGetAsync(string uriBaseAddress, string requestUri,
           Dictionary<string, string> headers, int timeoutMilliseconds, NetworkCredential credentials = null)
        {
            HttpResponseMessage ret = null;

            //------------------------------------------------------------------------------
            //-----------------------REMOVE THIS TO TEST 1 HTTPCLIENT-----------------------
            //------------------------------------------------------------------------------
            using (HttpClient client = new HttpClient())
            {
            //------------------------------------------------------------------------------
            //------------------------------------------------------------------------------
            //------------------------------------------------------------------------------

                HttpClientHandler handler = (credentials != null) ? new HttpClientHandler() { Credentials = credentials } : null;

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUri);
                request.RequestUri = new Uri(uriBaseAddress);

                if (headers != null && headers.Any())
                {
                    foreach (KeyValuePair<string, string> entry in headers)
                    {
                        request.Headers.Add(entry.Key, entry.Value);
                    }
                }

                CancellationTokenSource cts = new CancellationTokenSource();
                cts.CancelAfter(TimeSpan.FromMilliseconds(timeoutMilliseconds));
                try
                {
                    //------------------------------------------------------------------------------
                    //-----------------------SWITCH TO "_client" TO TEST 1 HTTPCLIENT---------------                    
                    //------------------------------------------------------------------------------
                    ret = client.SendAsync(request, cts.Token).Result;
                    //------------------------------------------------------------------------------
                    //------------------------------------------------------------------------------
                    //------------------------------------------------------------------------------
                }
                catch (TaskCanceledException ex)
                {
                    // Handle request being canceled due to timeout.
                    return ex.ToString();
                }

            //------------------------------------------------------------------------------
            //-----------------------REMOVE THIS TO TEST 1 HTTPCLIENT-----------------------
            //------------------------------------------------------------------------------
            }
            //------------------------------------------------------------------------------
            //------------------------------------------------------------------------------
            //------------------------------------------------------------------------------

            return await ret.Content.ReadAsStringAsync();
        }
        #endregion
    }
}
