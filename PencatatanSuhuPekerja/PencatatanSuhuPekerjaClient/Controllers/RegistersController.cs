using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PencatatanSuhuPekerjaAPI.ViewModels.AccountVM;

namespace PencatatanSuhuPekerjaClient.Controllers
{
    public class RegistersController : Controller
    {
        readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:44379/api/")
        };

        public IActionResult Index()
        {
            if (HttpContext.Session.IsAvailable)
            {
                if (HttpContext.Session.GetString("id") != null)
                {
                    return Redirect("/dashboards");
                }
            }
            return View();
        }

        public JsonResult Register(RegisterVM registerVM)
        {
            string stringData = JsonConvert.SerializeObject(registerVM);
            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");

            var resTask = client.PostAsync("accounts", contentData);
            resTask.Wait();

            var result = resTask.Result;

            var responseData = result.Content.ReadAsStringAsync().Result;

            return Json((result, responseData), new Newtonsoft.Json.JsonSerializerSettings());
        }
    }
}