using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PencatatanSuhuPekerjaAPI.Base;
using PencatatanSuhuPekerjaAPI.Models;
using PencatatanSuhuPekerjaAPI.Repositories.Data;

namespace PencatatanSuhuPekerjaAPI.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : BaseController<Department, DepartmentRepository>
    {
        readonly DepartmentRepository _department;
        public DepartmentsController(DepartmentRepository dep) : base(dep)
        {
            this._department = dep;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<int>> Update(string id, Department department)
        {
            var findId = await _department.GetId(id);
            findId.Name = department.Name;
            var data = await _department.Update(findId);
            if (data.Equals(null))
            {
                return BadRequest("Update Failed");
            }
            else
            {
                return data;
            }
        }
    }
}