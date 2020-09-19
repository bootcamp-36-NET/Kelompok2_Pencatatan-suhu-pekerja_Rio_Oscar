﻿using System;
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
            IEnumerable<EmployeeVM> employees = null;

            //var authToken = HttpContext.Session.GetString("JWToken");
            //client.DefaultRequestHeaders.Add("Authorization", authToken);

            var resTask = client.GetAsync("Employees");
            resTask.Wait();

            var result = resTask.Result;

            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<EmployeeVM>>();
                readTask.Wait();
                employees = readTask.Result;
            }

            return Json(employees, new Newtonsoft.Json.JsonSerializerSettings());
        }

        public IActionResult GetById(string id)
        {
            EmployeeVM employees = null;

            //var authToken = HttpContext.Session.GetString("JWToken");
            //client.DefaultRequestHeaders.Add("Authorization", authToken);

            var resTask = client.GetAsync("Employees/" + id);
            resTask.Wait();

            var result = resTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<EmployeeVM>();
                readTask.Wait();

                employees = readTask.Result;
            }

            return Json(employees, new Newtonsoft.Json.JsonSerializerSettings());
        }
        public IActionResult Edit(EditEmployeeVM editEmployeeVM)
        {
            string stringData = JsonConvert.SerializeObject(editEmployeeVM);
            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");

            //var authToken = HttpContext.Session.GetString("JWToken");
            //client.DefaultRequestHeaders.Add("Authorization", authToken);

            var resTask = client.PutAsync("Employees/" + editEmployeeVM.Id, contentData);

            var result = resTask.Result;
            var responseData = result.Content.ReadAsStringAsync().Result;

            return Json((result, responseData), new Newtonsoft.Json.JsonSerializerSettings());
        }

        public JsonResult Delete(string id)
        {
            //var authToken = HttpContext.Session.GetString("JWToken");
            //client.DefaultRequestHeaders.Add("Authorization", authToken);

            var resTask = client.DeleteAsync("Employees/" + id);
            resTask.Wait();
            var response = resTask.Result;

            return Json(response, new Newtonsoft.Json.JsonSerializerSettings());
        }
    }
}