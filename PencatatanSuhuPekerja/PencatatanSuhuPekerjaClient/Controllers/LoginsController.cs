using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PencatatanSuhuPekerjaAPI.ViewModels.AccountVM;

namespace PencatatanSuhuPekerjaClient.Controllers
{
    public class LoginsController : Controller
    {
        readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:44379/api/")
        };

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public IActionResult Login(LoginVM loginVM)
        {
            string stringData = JsonConvert.SerializeObject(loginVM);
            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");

            var resTask = client.PostAsync("accounts/login", contentData);

            var result = resTask.Result;
            var responseData = result.Content.ReadAsStringAsync().Result;   

            if (result.IsSuccessStatusCode)
            {
                var token = new JwtSecurityToken(jwtEncodedString: responseData);
                var authToken = "Bearer " + responseData;
                var isVerified = token.Claims.First(c => c.Type == "IsVerified").Value;

                HttpContext.Session.SetString("id", token.Claims.First(c => c.Type == "Id").Value);
                HttpContext.Session.SetString("email", token.Claims.First(c => c.Type == "Email").Value);
                HttpContext.Session.SetString("roles", token.Claims.First(c => c.Type == "Roles").Value);
                HttpContext.Session.SetString("verified", token.Claims.First(c => c.Type == "IsVerified").Value);
                HttpContext.Session.SetString("JWToken", authToken);

                return Json((result,responseData, isVerified), new Newtonsoft.Json.JsonSerializerSettings());
            }
            return Json((result, responseData), new Newtonsoft.Json.JsonSerializerSettings());
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("/logins");
        }
    }
}