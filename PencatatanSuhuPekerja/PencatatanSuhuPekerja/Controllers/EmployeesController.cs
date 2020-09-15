using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PencatatanSuhuPekerjaAPI.Repositories;
using PencatatanSuhuPekerjaAPI.ViewModels.AccountVM;

namespace PencatatanSuhuPekerjaAPI.Controllers
{
    //[Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly AccountRepository _accountRepository;

        public EmployeesController(AccountRepository accountRepository)
        {
            this._accountRepository = accountRepository;
        }

        // GET: Employees/
        [HttpGet]
        public async Task<ActionResult> GetAllEmployee()
        {
            var allEmployee = await _accountRepository.GetAllEmployee();
            return Ok(allEmployee);
        }

        // GET: Employees/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult> GetUserById(string id)
        {
            var selectedEmployee = await _accountRepository.GetProfile(id);
            return Ok(selectedEmployee);
        }

        // PUT: Employees/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> EditEmployee(string id, EditEmployeeVM model)
        {
            var result = await _accountRepository.EditEmployee(id, model);
            if (result != null)
            {
                return BadRequest(result);
            }
            return Ok();
        }

        // DELETE: Employees/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(string id)
        {
            var result = await _accountRepository.DeactivateEmpoyee(id);
            if (result != null)
            {
                return BadRequest(result);
            }

            return Ok();
        }
    }
}