using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoRepository.Interfaces;
using UniversalWebApi.Helpers.ExceptionManager;

namespace UniversalWebApi.Controllers.BaseControllers
{
    public class MongoDbController<T> : Controller where T : class, IMongoDoc
    {
        //protected readonly IMongoRepository Db;
        //protected readonly IExceptionManager ExceptionManager;

        //public MongoDbController(IMongoRepository db, IExceptionManager exceptionManager)
        //{
        //    Db = db;
        //    ExceptionManager = exceptionManager;
        //}

        protected IMongoRepository Db => (IMongoRepository)HttpContext.RequestServices.GetService(typeof(IMongoRepository));

        [HttpGet("get")]
        public virtual async Task<IActionResult> Get()
        {
            var result = await Db.GetAsync<T>();
            return result != null ? (IActionResult) Ok(result) : BadRequest();
        }

        [HttpGet("get/{id}")]
        public virtual async Task<IActionResult> Get(ObjectId id)
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
                await Db.AddAsync(entity);
                return Ok($"{typeof(T).Name} added successfully!");
            }
            catch (Exception e)
            {
                var controllerContext = ControllerContext.ActionDescriptor;
                //await ExceptionManager.Log(e, controllerContext.ControllerName, controllerContext.ActionName);
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
                //await ExceptionManager.Log(e, controllerContext.ControllerName, controllerContext.ActionName);
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        public virtual async Task<IActionResult> Delete(ObjectId id)
        {
            try
            {
                await Db.RemoveAsync<T>(id);
                return Ok($"{typeof(T).Name} with id: {id} deleted successfully!");
            }
            catch (Exception e)
            {
                var controllerContext = ControllerContext.ActionDescriptor;
                //await ExceptionManager.Log(e, controllerContext.ControllerName, controllerContext.ActionName);
                return BadRequest(e.Message);
            }
        }
    }
}