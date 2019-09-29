using UniversalWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using SqlRepository.Interfaces;
using UniversalWebApi.Controllers.BaseControllers;
using UniversalWebApi.Helpers.Serializer;

namespace UniversalWebApi.Controllers
{
    //[ServiceFilter(typeof(ApiAsyncActionFilter))]
    [Route("api/[controller]")]
    public class UserController : SerializeController<User>
    {
        public UserController(IDataRepository db, ISerializeHelper manager) : base(db, manager)
        {
        }

//        [HttpPut("update")]
//        public override async Task<IActionResult> Update([FromBody] User user)
//        {
//            try
//            {
//                var result = Db.Get<User>(user.Id);
//
//                if (result == null)
//                    return BadRequest($"No {typeof(User).Name} to update");
//
//                result.Name = user.Name;
//                result.Age = user.Age;
//                result.Email = user.Email;
//
//                await Db.UpdateAsync(result);
//                return Ok($"{typeof(User).Name} updated successfully!");
//            }
//            catch (Exception e)
//            {
//                var controllerContext = ControllerContext.ActionDescriptor;
//                await ExceptionManager.Log(e, controllerContext.ControllerName, controllerContext.ActionName);
//                return BadRequest(e.Message);
//            }
//        }
//
//        [HttpDelete("delete/{id}")]
//        public override async Task<IActionResult> Delete(int id)
//        {
//            try
//            {
//                var result = Db.Get<User>(id);
//
//                if (result == null)
//                    return BadRequest($"No {typeof(User).Name} with id: {id} to delete");
//                
//                await Db.DeleteRowAsync<User>(id);
//                return Ok($"{typeof(User).Name} with id: {id} deleted successfully!");
//            }
//            catch (Exception e)
//            {
//                var controllerContext = ControllerContext.ActionDescriptor;
//                await ExceptionManager.Log(e, controllerContext.ControllerName, controllerContext.ActionName);
//                return BadRequest(e.Message);
//            }
//        }
    }
}