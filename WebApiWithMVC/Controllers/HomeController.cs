using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApiWithMVC.Models;

namespace WebApiWithMVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private const string url = "https://jsonplaceholder.typicode.com";
        private static string urlParameters = "/posts";
        public ActionResult Index()
        {
            var data = CallWebApi(url, urlParameters);
            return View(data);
        }
      
        public ActionResult About()
        {
            var ApiPost = new ApiViewModel()
            {
                UserId = "321",
                Id = "101",
                Title = "Lorem ipsum dolor sit amet",
                Body = "consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat."
            };
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                var content = new StringContent(JsonConvert.SerializeObject(ApiPost).ToString(),Encoding.UTF8, "application/json");
                
                HttpResponseMessage responsePost = client.PostAsJsonAsync("/posts", content).Result;
                //ViewBag.Message = responsePost;
                //return View();
                if (responsePost.IsSuccessStatusCode)
                {
                    Uri returnUrl = responsePost.Headers.Location;
                    Console.WriteLine(returnUrl);
                }

            }
            var data = CallWebApi(url, "/posts/321");
            return View(data);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public List<ApiViewModel> CallWebApi(string url, string urlParameter)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync(urlParameter).Result;
                if (response.IsSuccessStatusCode)
                {

                    var JsonStr = response.Content.ReadAsStringAsync().Result;
                    //var JsonStr = response.Content.ReadAsAsync<IEnumerable<ApiViewModel>>().Result;
                    //ViewBag.Result  = JsonConvert.DeserializeObject<List<ApiViewModel>>(JsonStr);
                    return JsonConvert.DeserializeObject < List<ApiViewModel>>(JsonStr);
                    //ViewBag.Result = JsonConvert.DeserializeObject<ApiViewModel>(JsonStr);
                }

            }
            return null;
        }

    }
}