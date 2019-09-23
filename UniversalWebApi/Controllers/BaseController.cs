using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SqlRepository.Interfaces;

namespace UniversalWebApi.Controllers
{
    public class BaseController<T> : Controller where T : class
    {
        protected readonly IDataRepository Db;
        public BaseController(IDataRepository db) => Db = db;

        [HttpGet("getAsync")]
        public virtual async Task<IEnumerable<T>> GetAsync() => await Db.GetAllAsync<T>();

        [HttpGet("get")]
        public virtual IEnumerable<T> Get() => Db.GetAll<T>();
        
        [HttpGet("getAsync/{id}")]
        public virtual async Task<T> GetAsync(int id) => await Db.GetAsync<T>(id);
        
        [HttpGet("get/{id}")]
        public virtual T Get(int id) => Db.Get<T>(id);

        [HttpPost("add")]
        public virtual async Task<IActionResult> Add([FromBody] T entity)
        {
            try
            {
                await Db.InsertAsync(entity);
                return Ok("Added successfully!");
            }
            catch (Exception e)
            {
                return Json(e);
            }
        }

        [HttpPut("update")]
        public virtual async Task<IActionResult> Update([FromBody] T entity)
        {
            try
            {
                await Db.UpdateAsync(entity);
                return Ok("Updated successfully!");
            }
            catch (Exception e)
            {
                return Json(e);
            }
        }

        [HttpDelete("delete/{id}")]
        public virtual async Task<IActionResult> Delete(int id)
        {
            try
            {
                await Db.DeleteRowAsync<T>(id);
                return Ok("Deleted successfully!");
            }
            catch (Exception e)
            {
                return Json(e);
            }
        }
    }
}