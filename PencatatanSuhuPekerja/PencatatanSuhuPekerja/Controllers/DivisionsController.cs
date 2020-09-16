using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PencatatanSuhuPekerjaAPI.Base;
using PencatatanSuhuPekerjaAPI.Context;
using PencatatanSuhuPekerjaAPI.Models;
using PencatatanSuhuPekerjaAPI.Repositories.Data;

namespace PencatatanSuhuPekerjaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DivisionsController : BaseController<Division, DivisionRepository>
    {
        readonly DivisionRepository _division; 
        public DivisionsController(DivisionRepository divisionRepository) : base(divisionRepository)
        {
            this._division = divisionRepository;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<int>> Update(string id, Division division)
        {
            var findId = await _division.GetId(id);
            findId.Name = division.Name;
            findId.DepartmentId = division.DepartmentId;
            var data = await _division.Update(findId);
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