using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PencatatanSuhuPekerjaAPI.Models;
using PencatatanSuhuPekerjaAPI.ViewModels.AccountVM;
using PencatatanSuhuPekerjaAPI.ViewModels.EmployeeVM;

namespace PencatatanSuhuPekerjaClient.Controllers
{
    public class EmployeesController : Controller
    {
        readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:44379/api/")
        };

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult LoadEmployee()
        {
            IEnumerable<Employee> employees = null;

            var authToken = HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Add("Authorization", authToken);

            var resTask = client.GetAsync("Employees");
            resTask.Wait();

            var result = resTask.Result;

            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<Employee>>();
                readTask.Wait();
                employees = readTask.Result;
            }

            return Json(employees, new Newtonsoft.Json.JsonSerializerSettings());
        }

        public IActionResult GetById(string id)
        {
            UserProfileVM employees = null;

            var authToken = HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Add("Authorization", authToken);

            var resTask = client.GetAsync("Employees/" + id);
            resTask.Wait();

            var result = resTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<UserProfileVM>();
                readTask.Wait();

                employees = readTask.Result;
            }

            return Json(employees, new Newtonsoft.Json.JsonSerializerSettings());
        }
        public IActionResult Edit(EditEmployeeVM editEmployeeVM)
        {
            var id = HttpContext.Session.GetString("id");

            string stringData = JsonConvert.SerializeObject(editEmployeeVM);
            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");

            var authToken = HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Add("Authorization", authToken);

            var resTask = client.PutAsync("Employees/" + id, contentData);

            var result = resTask.Result;
            var responseData = result.Content.ReadAsStringAsync().Result;

            if (result.IsSuccessStatusCode)
            {
                HttpContext.Session.SetString("email", editEmployeeVM.Email);
            }

            return Json((result, responseData), new Newtonsoft.Json.JsonSerializerSettings());
        }

        public JsonResult Delete(string id)
        {
            var authToken = HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Add("Authorization", authToken);

            var resTask = client.DeleteAsync("Employees/" + id);
            resTask.Wait();
            var response = resTask.Result;

            return Json(response, new Newtonsoft.Json.JsonSerializerSettings());
        }
    }
}