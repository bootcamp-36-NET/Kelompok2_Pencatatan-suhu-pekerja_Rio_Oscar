﻿using System;
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

                HttpContext.Session.SetString("id", token.Claims.First(c => c.Type == "Id").Value);
                HttpContext.Session.SetString("email", token.Claims.First(c => c.Type == "Email").Value);
                HttpContext.Session.SetString("roles", token.Claims.First(c => c.Type == "Roles").Value);
                HttpContext.Session.SetString("verified", token.Claims.First(c => c.Type == "IsVerified").Value);
                HttpContext.Session.SetString("JWToken", authToken);

                //client.DefaultRequestHeaders.Add("Authorization", authToken);

                return Json((result,responseData), new Newtonsoft.Json.JsonSerializerSettings());
            }
            return Json((result, responseData), new Newtonsoft.Json.JsonSerializerSettings());
        }

        [HttpGet]
        // GET: Edit
        public ActionResult Edit()
        {
            return View("Edit");
        }

        //public ActionResult GetEdit()
        //{
        //    var id = HttpContext.Session.GetString("id");
        //    UserEditViewModel userEditViewModel = null;

        //    var resTask = client.GetAsync("Users/Edit/" + id);
        //    resTask.Wait();

        //    var result = resTask.Result;
        //    if (result.IsSuccessStatusCode)
        //    {
        //        var readTask = result.Content.ReadAsAsync<UserEditViewModel>();
        //        readTask.Wait();

        //        userEditViewModel = readTask.Result;
        //    }
        //    var response = Tuple.Create(userEditViewModel, result);

        //    return Json(response, new Newtonsoft.Json.JsonSerializerSettings());
        //}

        //[HttpPost]
        //public IActionResult Edit(UserEditViewModel userEditViewModel)
        //{
        //    UserViewModel account = null;
        //    var id = HttpContext.Session.GetString("id");

        //    string stringData = JsonConvert.SerializeObject(userEditViewModel);
        //    var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
        //    var resTask = client.PutAsync("Users/Edit/" + id, contentData);

        //    var result = resTask.Result;
        //    if (result.IsSuccessStatusCode)
        //    {
        //        var data = result.Content.ReadAsStringAsync().Result;
        //        var json = JsonConvert.DeserializeObject(data).ToString();
        //        account = JsonConvert.DeserializeObject<UserViewModel>(json);

        //        HttpContext.Session.SetString("uname", account.UserName);
        //    }
        //    var response = Tuple.Create(account, result);
        //    return Json(response, new Newtonsoft.Json.JsonSerializerSettings());
        //}

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("/login");
        }
    }
}