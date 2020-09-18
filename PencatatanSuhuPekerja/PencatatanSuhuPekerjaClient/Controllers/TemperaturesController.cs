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
    public class TemperaturesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:44379/api/")
        };

        public IActionResult LoadTemperatures()
        {
            //if (HttpContext.Session.GetString("JWToken") != null)
            //{
                //client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("JWToken"));
                IEnumerable<Temperature> temperatures;
                var restask = client.GetAsync("temperatures");
                restask.Wait();
                var result = restask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<Temperature>>();
                    readTask.Wait();
                    temperatures = readTask.Result;
                }
                else
                {
                    temperatures = Enumerable.Empty<Temperature>();
                    ModelState.AddModelError(string.Empty, "Error Load Departments");
                }
                return Json(temperatures);
            //}
            //else
            //{
                //return Redirect("/error");
            //}

        }
        public IActionResult GetTemperatures(string id)
        {
            //client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("token"));
            Temperature temperature;
            var restask = client.GetAsync("temperatures/" + id);
            restask.Wait();
            var result = restask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<Temperature>();
                readTask.Wait();
                temperature = readTask.Result;
            }
            else
            {
                temperature = null;
                ModelState.AddModelError(string.Empty, "Error Load Department");
            }
            return Json(temperature);
        }

        public IActionResult InsertOrUpdateDivision(string id, Temperature temperature)
        {
            try
            {
                //client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("token"));
                var json = JsonConvert.SerializeObject(temperature);
                var buffer = System.Text.Encoding.UTF8.GetBytes(json);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                if (temperature.TemperatureId == null)
                {
                    var result = client.PostAsync("temperatures", byteContent).Result;
                    return Ok(200);
                }
                else if (temperature.TemperatureId == id)
                {
                    var result = client.PutAsync("temperatures/" + id, byteContent).Result;
                    return Ok(200);
                }
                return BadRequest(404);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IActionResult DeleteTemperature(string id)
        {
            //client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("token"));
            var result = client.DeleteAsync("temperatures/" + id).Result;
            if (result.IsSuccessStatusCode)
            {
                return Ok(200);
            }
            return BadRequest(500);
        }
    }
}