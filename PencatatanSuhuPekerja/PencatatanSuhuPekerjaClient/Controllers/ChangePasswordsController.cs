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
    public class ChangePasswordsController : Controller
    {
        readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:44379/api/")
        };

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ChangePassword(ChangePasswordVM changePasswordVM)
        {
            var id = HttpContext.Session.GetString("id");

            string stringData = JsonConvert.SerializeObject(changePasswordVM);
            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");

            var authToken = HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Add("Authorization", authToken);

            var resTask = client.PutAsync("Accounts/ChangePassword/" + id, contentData);

            var result = resTask.Result;
            var responseData = result.Content.ReadAsStringAsync().Result;

            return Json((result, responseData), new Newtonsoft.Json.JsonSerializerSettings());
        }
    }
}