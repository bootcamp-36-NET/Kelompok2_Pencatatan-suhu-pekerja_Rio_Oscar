using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PencatatanSuhuPekerjaAPI.Context;
using PencatatanSuhuPekerjaAPI.Models;
using PencatatanSuhuPekerjaAPI.Services;

namespace PencatatanSuhuPekerjaAPI.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class TemperaturesController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly MyContext _context;
        private readonly SendEmailService _sendEmailService;

        public TemperaturesController(IConfiguration myConfiguration, MyContext myContext, SendEmailService sendEmailService)
        {
            _configuration = myConfiguration;
            _context = myContext;
            _sendEmailService = sendEmailService;
        }

        // GET: api/Temperatures
        [HttpGet]
        public async Task<List<Temperature>> GetAll()
        {
            List<Temperature> list = new List<Temperature>();
            var temperature = await _context.Temperatures.Include("Employee").ToListAsync();
            if (temperature.Count == 0)
            {
                return null;
            } 
            return temperature;
        }

        // GET: api/Temperatures/5
        [HttpGet("{id}", Name = "Get")]
        public Temperature GetId(string id)
        {
            var temperature = _context.Temperatures.Include("Employee").SingleOrDefault(x => x.TemperatureId == id);
            return temperature;
        }

        // POST: api/Temperatures
        [HttpPost]
        public IActionResult Create(Temperature temperature)
        {
            var employee = _context.Employees.Include("User").SingleOrDefault(x => x.EmployeeId == temperature.EmployeeId);
            var suhu = temperature.EmployeeTemperature;
            var suhus = Convert.ToDouble(suhu);
            if (suhus >= 37.5 )
            {
                _sendEmailService.SendEmail(employee.User.Email, suhus, employee);
            }
            temperature.Date = DateTimeOffset.Now;
            _context.Temperatures.Add(temperature);
            _context.SaveChanges();
            return Ok("Data Created Successfully");
        }

        // PUT: api/Temperatures/5
        [HttpPut("{id}")]
        public IActionResult Update(string id, Temperature temperature)
        {
            var oldTemperature = _context.Temperatures.Find(id);
            oldTemperature.Date = DateTimeOffset.Now; 
            oldTemperature.EmployeeTemperature = temperature.EmployeeTemperature;
            oldTemperature.EmployeeId = temperature.EmployeeId;
            _context.SaveChanges();
            return Ok("Update Successfull");
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var temperature = _context.Temperatures.SingleOrDefault(x => x.TemperatureId == id);
            if (temperature != null)
            {
                _context.Temperatures.Remove(temperature);
                _context.SaveChanges();
                return Ok("Delete Successfully");
            }
            return Ok("Delete failed");
        }
    }
}
