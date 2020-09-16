using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PencatatanSuhuPekerjaAPI.Repositories;
using PencatatanSuhuPekerjaAPI.ViewModels.AccountVM;
using PencatatanSuhuPekerjaAPI.ViewModels.EmployeeVM;

namespace PencatatanSuhuPekerjaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {

        private readonly AccountRepository _accountRepository;

        public AccountsController(AccountRepository accountRepository)
        {
            this._accountRepository = accountRepository;
        }

        // POST: Accounts/
        [HttpPost]
        public async Task<ActionResult> Register(RegisterVM registerVM)
        {
            if (registerVM.Password != registerVM.ConfirmPassword)
            {
                return BadRequest("Password And Confirm Password Is Not Match !");
            }

            var result = await _accountRepository.Register(registerVM);
            if(result != null)
            {
                return BadRequest(result);
            }

            return Ok("Successfully Registered !");
        }

        // POST: Accounts/login
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login(LoginVM loginVM)
        {
            var (errMessage, token) = await _accountRepository.Login(loginVM);
            if (errMessage != null)
            {
                return BadRequest(errMessage);
            }

            return Ok(token);
        }

        // POST: Accounts/verify/{id}
        [HttpPost]
        [Route("verify/{id}")]
        public async Task<ActionResult> Verify(string id, [FromBody]string code)
        {
            var result = await _accountRepository.Verify(id,code);
            if (result != null)
            {
                return BadRequest(result);
            }

            return Ok("Successfully Registered !");
        }

        // PUT: Accounts/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> EditProfile(string id, EditEmployeeVM editEmployeeVM)
        {
            var result = await _accountRepository.EditProfile(id, editEmployeeVM);
            if (result != null)
            {
                return BadRequest(result);
            }

            return Ok("Successfully Registered !");
        }

        // PUT: Accounts/ChangePassword/{id}
        [HttpPut("ChangePassword/{id}")]
        public async Task<ActionResult> ChangePassowrd(string id, ChangePassowordVM changePassowordVM)
        {
            var result = await _accountRepository.ChangePassword(id, changePassowordVM);
            if (result != null)
            {
                return BadRequest(result);
            }

            return Ok("Successfully Registered !");
        }
    }
}