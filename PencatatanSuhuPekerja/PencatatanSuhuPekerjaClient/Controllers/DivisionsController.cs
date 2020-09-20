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
    public class DivisionsController : Controller
    {
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

        public IActionResult LoadDivisions()
        {
            //if (HttpContext.Session.GetString("JWToken") != null)
            //{
            client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("JWToken"));
            IEnumerable<Division> divisions;
            var restask = client.GetAsync("divisions");
            restask.Wait();
            var result = restask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<List<Division>>();
                readTask.Wait();
                divisions = readTask.Result;
            }
            else
            {
                divisions = Enumerable.Empty<Division>();
                ModelState.AddModelError(string.Empty, "Error Load Departments");
            }
            return Json(divisions);
            //}
            //else
            //{
            //return Redirect("/error");
            //}

        }
        public IActionResult GetDivision(string id)
        {
            client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("JWToken"));
            Division division;
            var restask = client.GetAsync("divisions/" + id);
            restask.Wait();
            var result = restask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<Division>();
                readTask.Wait();
                division = readTask.Result;
            }
            else
            {
                division = null;
                ModelState.AddModelError(string.Empty, "Error Load Department");
            }
            return Json(division);
        }

        public IActionResult InsertOrUpdateDivision(string id, Division division)
        {
            try
            {
                client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("JWToken"));
                var json = JsonConvert.SerializeObject(division);
                var buffer = System.Text.Encoding.UTF8.GetBytes(json);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                if (division.Id == null)
                {
                    var result = client.PostAsync("divisions", byteContent).Result;
                    return Ok(200);
                }
                else if (division.Id == id)
                {
                    var result = client.PutAsync("divisions/" + id, byteContent).Result;
                    return Ok(200);
                }
                return BadRequest(404);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IActionResult DeleteDivision(string id)
        {
            client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("JWToken"));
            var result = client.DeleteAsync("divisions/" + id).Result;
            if (result.IsSuccessStatusCode)
            {
                return Ok(200);
            }
            return BadRequest(500);
        }

        public IActionResult GetDivisionByDepartment(string id)
        {
            client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("JWToken"));
            IEnumerable<Division> divisions;
            var restask = client.GetAsync("Divisions/getDivisionByDepartment/" + id);
            restask.Wait();
            var result = restask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IEnumerable<Division>>();
                readTask.Wait();
                divisions = readTask.Result;
            }
            else
            {
                divisions = null;
                ModelState.AddModelError(string.Empty, "Error Load Divisions");
            }
            return Json(divisions);
        }
    }
}