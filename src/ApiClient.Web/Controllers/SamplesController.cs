using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace ApiClient.Web.Controllers
{
    [Authorize]
    public class SamplesController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
        
        public async Task<IActionResult> CallIdentityApi()
        {
            var accessToken = await HttpContext.Authentication.GetTokenAsync("access_token");

            var client = new HttpClient();
            client.SetBearerToken(accessToken);
            var response = await client.GetAsync("http://localhost:5001/identity");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            ViewBag.Json = JArray.Parse(content).ToString();
            return View("json");
        }
    }
}