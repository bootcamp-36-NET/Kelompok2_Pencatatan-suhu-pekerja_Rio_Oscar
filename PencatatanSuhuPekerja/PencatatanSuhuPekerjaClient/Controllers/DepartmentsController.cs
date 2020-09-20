using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PencatatanSuhuPekerjaAPI.Models;

namespace PencatatanSuhuPekerjaClient.Controllers
{
    public class DepartmentsController : Controller
    {
        [Route("departments")]
        public IActionResult Index()
        {
            if (!HttpContext.Session.IsAvailable)
            {
                return Redirect("/logins");
            }
            if (HttpContext.Session.GetString("verified") == "false")
            {
                return Redirect("/verifies");
            }
            if (HttpContext.Session.GetString("roles") == null)
            {
                return Redirect("/logins");
            }
            var stringRole = HttpContext.Session.GetString("roles");
            var roles = stringRole.Split(',').ToList();
            foreach (var role in roles)
            {
                if (role == "ADMIN")
                {
                    return View();
                }
            }
            return Redirect("/error");
        }
        readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:44379/api/")
        };


        public IActionResult LoadDepartments()
        {
            //if (HttpContext.Session.GetString("JWToken") != null)
            //{
            client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("JWToken"));
            IEnumerable<Department> departments;
            var restask = client.GetAsync("departments");
            restask.Wait();
            var result = restask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<List<Department>>();
                readTask.Wait();
                departments = readTask.Result;
            }
            else
            {
                departments = Enumerable.Empty<Department>();
                ModelState.AddModelError(string.Empty, "Error Load Departments");
            }
            return Json(departments);
            //}
            //else
            //{
            //    return redirect("/error");
            //}

        }
        public IActionResult GetDepartment(string id)
        {
            client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("JWToken"));
            Department department;
            var restask = client.GetAsync("departments/" + id);
            restask.Wait();
            var result = restask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<Department>();
                readTask.Wait();
                department = readTask.Result;
            }
            else
            {
                department = null;
                ModelState.AddModelError(string.Empty, "Error Load Department");
            }
            return Json(department);
        }

        public IActionResult InsertOrUpdateDepartment(string id, Department department)
        {
            try
            {
                client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("JWToken"));
                var json = JsonConvert.SerializeObject(department);
                var buffer = System.Text.Encoding.UTF8.GetBytes(json);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                if (department.Id == null)
                {
                    var result = client.PostAsync("departments", byteContent).Result;
                    return Ok(200);
                }
                else if (department.Id == id)
                {
                    var result = client.PutAsync("departments/" + id, byteContent).Result;
                    return Ok(200);
                }
                return BadRequest(404);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IActionResult DeleteDepartment(string id)
        {
            client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("JWToken"));
            var result = client.DeleteAsync("departments/" + id).Result;
            if (result.IsSuccessStatusCode)
            {
                return Ok(200);
            }
            return BadRequest(500);
        }
    }
}