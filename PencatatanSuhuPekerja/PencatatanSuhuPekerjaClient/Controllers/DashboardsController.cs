using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PencatatanSuhuPekerjaClient.Controllers
{
    public class DashboardsController : Controller
    {
        readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:44379/api/")
        };

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LoadChartData()
        {
            var authToken = HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Add("Authorization", authToken);

            List<int> chartData = null;
            var restask = client.GetAsync("Dashboards");
            restask.Wait();
            var result = restask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<List<int>>();
                readTask.Wait();
                chartData = readTask.Result;
            }

            return Json((result,chartData), new Newtonsoft.Json.JsonSerializerSettings());

        }
    }
}