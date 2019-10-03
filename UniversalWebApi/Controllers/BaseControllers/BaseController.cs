using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SqlRepository.Interfaces;
using SqlRepository.Repositories;
using UniversalWebApi.Helpers.ExceptionManager;
using UniversalWebApi.Helpers.Filters;

namespace UniversalWebApi.Controllers.BaseControllers
{
    [ServiceFilter(typeof(ApiExceptionFilter))]
    public class BaseController<T> : Controller where T : class
    {
        protected IDataRepository Db => (IDataRepository)HttpContext.RequestServices.GetService(typeof(IDataRepository));

        [HttpGet("get")]
        public virtual async Task<JsonResult> Get()
        {
            var result = await Db.GetAllAsync<T>();

            if(result == null)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json($"Server Error");
            }

            HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
            return Json(result);
        }

        [HttpGet("get/{id}")]
        public virtual async Task<JsonResult> Get(int id)
        {
            var result = await Db.GetAsync<T>(id);

            if (result == null)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json($"There is no {typeof(T).Name} with id = {id}");
            }

            HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
            return Json(result);
        }

        [HttpPost("add")]
        public virtual async Task<JsonResult> Add([FromBody] T entity)
        {
            if (!ModelState.IsValid)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                return Json($"Data error");
            }

            await Db.InsertAsync(entity);
            HttpContext.Response.StatusCode = (int)HttpStatusCode.Created;
            return Json($"{typeof(T).Name} added successfully!");
        }

        [HttpPut("update")]
        public virtual async Task<IActionResult> Update([FromBody] T entity)
        {
            if (!ModelState.IsValid)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                return Json($"Data error");
            }

            await Db.UpdateAsync(entity);
            HttpContext.Response.StatusCode = (int)HttpStatusCode.NoContent;
            return Json($"{typeof(T).Name} updated successfully!");
        }

        [HttpDelete("delete/{id}")]
        public virtual async Task<JsonResult> Delete(int id)
        {
            await Db.DeleteRowAsync<T>(id);
            HttpContext.Response.StatusCode = (int)HttpStatusCode.NoContent;
            return Json($"{typeof(T).Name} with id: {id} deleted successfully!");
        }
    }
}