using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PencatatanSuhuPekerjaClient.Controllers
{
    public class VerifiesController : Controller
    {
        readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:44379/api/")
        };

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Verify(string code)
        {
            var id = HttpContext.Session.GetString("id");

            var contentData = new StringContent(code, System.Text.Encoding.UTF8, "application/json");

            var authToken = HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Add("Authorization", authToken);

            var resTask = client.PostAsync("accounts/verify/" + id, contentData);
            resTask.Wait();

            var result = resTask.Result;
            var responseData = result.Content.ReadAsStringAsync().Result;
            if (result.IsSuccessStatusCode)
            {
                HttpContext.Session.SetString("IsVerified", "true");

                return Json((result,responseData), new Newtonsoft.Json.JsonSerializerSettings());
            }

            return Json((result, responseData), new Newtonsoft.Json.JsonSerializerSettings());
        }
    }
}