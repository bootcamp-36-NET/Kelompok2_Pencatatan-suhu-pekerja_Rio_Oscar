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
    public class UserProfilesController : Controller
    {
        readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:44379/api/")
        };

        public IActionResult Index()
        {
            if (!HttpContext.Session.IsAvailable)
            {
                return Redirect("/logins");
            }
            if (HttpContext.Session.GetString("id") == null)
            {
                return Redirect("/logins");
            }
            if (HttpContext.Session.GetString("verified") == "false")
            {
                return Redirect("/verifies");
            }
            return View();
        }

        public ActionResult GetUserProfile()
        {
            var id = HttpContext.Session.GetString("id");
            UserProfileVM userProfileVM = null;

            var authToken = HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Add("Authorization", authToken);

            var resTask = client.GetAsync("Accounts/" + id);
            resTask.Wait();

            var result = resTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<UserProfileVM>();
                readTask.Wait();

                userProfileVM = readTask.Result;
            }

            return Json((result, userProfileVM), new Newtonsoft.Json.JsonSerializerSettings());
        }
    }
}