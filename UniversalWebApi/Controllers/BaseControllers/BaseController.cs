using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SqlRepository.Interfaces;
using UniversalWebApi.Helpers.ExceptionManager;

namespace UniversalWebApi.Controllers.BaseControllers
{
    public class BaseController<T> : Controller where T : class
    {
        protected readonly IDataRepository Db;
        protected readonly IExceptionManager ExceptionManager;

        public BaseController(IDataRepository db, IExceptionManager exceptionManager)
        {
            Db = db;
            ExceptionManager = exceptionManager;
        }
        
        [HttpGet("get")]
        public virtual IActionResult Get()
        {
            var result = Db.GetAll<T>();
            return result != null ? (IActionResult) Ok(result) : BadRequest();
        }
        
        [HttpGet("getAsync")]
        public virtual async Task<IActionResult> GetAsync()
        {
            var result = await Db.GetAllAsync<T>();
            return result != null ? (IActionResult) Ok(result) : BadRequest();
        }

        [HttpGet("get/{id}")]
        public virtual IActionResult Get(int id)
        {
            var result = Db.Get<T>(id);
            return result != null
                ? (IActionResult) Ok(result)
                : BadRequest($"There is no {typeof(T).Name} with id = {id}");
        }

        [HttpGet("getAsync/{id}")]
        public virtual async Task<IActionResult> GetAsync(int id)
        {
            var result = await Db.GetAsync<T>(id);
            return result != null
                ? (IActionResult) Ok(result)
                : BadRequest($"There is no {typeof(T).Name} with id = {id}");
        }

        [HttpPost("add")]
        public virtual async Task<IActionResult> Add([FromBody] T entity)
        {
            try
            {
                await Db.InsertAsync(entity);
                return Ok($"{typeof(T).Name} added successfully!");
            }
            catch (Exception e)
            {
                var controllerContext = ControllerContext.ActionDescriptor;
                await ExceptionManager.Log(e, controllerContext.ControllerName, controllerContext.ActionName);
                return BadRequest(e.Message);
            }
        }

        [HttpPut("update")]
        public virtual async Task<IActionResult> Update([FromBody] T entity)
        {
            try
            {
                await Db.UpdateAsync(entity);
                return Ok($"{typeof(T).Name} updated successfully!");
            }
            catch (Exception e)
            {
                var controllerContext = ControllerContext.ActionDescriptor;
                await ExceptionManager.Log(e, controllerContext.ControllerName, controllerContext.ActionName);
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        public virtual async Task<IActionResult> Delete(int id)
        {
            try
            {
                await Db.DeleteRowAsync<T>(id);
                return Ok($"{typeof(T).Name} with id: {id} deleted successfully!");
            }
            catch (Exception e)
            {
                var controllerContext = ControllerContext.ActionDescriptor;
                await ExceptionManager.Log(e, controllerContext.ControllerName, controllerContext.ActionName);
                return BadRequest(e.Message);
            }
        }
    }
}