using System.Diagnostics;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PruebaHttpClient.Services;

namespace PruebaHttpClient.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        public static HttpClient _client;
        public static IServices _services;

        public ValuesController(HttpClient client, IServices services)
        {
            _client = client;
            _services = services;
        }

        // GET api/values
        [HttpGet("{username}")]
        public async Task<string> Get(string username)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            List<string>[] ret = await _services.sendQueryAsync(username);
            watch.Stop();
            return watch.ElapsedMilliseconds.ToString();
        }

    }
}
