using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PencatatanSuhuPekerjaAPI.ViewModels.EmployeeVM;

namespace PencatatanSuhuPekerjaClient.Controllers
{
    public class EditUserProfilesController : Controller
    {
        readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:44379/api/")
        };

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Edit(EditEmployeeVM editEmployeeVM)
        {
            var id = HttpContext.Session.GetString("id");

            string stringData = JsonConvert.SerializeObject(editEmployeeVM);
            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");

            var authToken = HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Add("Authorization", authToken);

            var resTask = client.PutAsync("Accounts/" + id, contentData);

            var result = resTask.Result;
            var responseData = result.Content.ReadAsStringAsync().Result;

            if (result.IsSuccessStatusCode)
            {
                HttpContext.Session.SetString("email", editEmployeeVM.Email);
            }

            return Json((result, responseData), new Newtonsoft.Json.JsonSerializerSettings());
        }
    }
}