using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PencatatanSuhuPekerjaAPI.Repositories.Interfaces;

namespace PencatatanSuhuPekerjaAPI.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<TEntity, TRepository> : ControllerBase
        where TEntity : class
        where TRepository : IRepository<TEntity>
    {
        IRepository<TEntity> _repository;
        public BaseController(TRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public virtual async Task<IEnumerable<TEntity>> GetAll() => await _repository.GetAll();

        [HttpGet("{id}")]
        public async Task<ActionResult<TEntity>> GetId(string id) => await _repository.GetId(id);

        [HttpPost]
        public async Task<ActionResult<TEntity>> Create(TEntity entity)
        {
            var data = await _repository.Create(entity);
            if (data > 0)
            {
                return Ok("Create successfully");
            }
            else
            {
                return BadRequest("Create Failed");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> Delete(string id)
        {
            var data = await _repository.Delete(id);
            if (data.Equals(null))
            {
                return Ok("Delete failed");
            }
            else
            {
                return data;
            }
        }
    }
}