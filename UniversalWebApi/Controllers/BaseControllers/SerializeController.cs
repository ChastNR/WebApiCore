using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SqlRepository.Interfaces;
using UniversalWebApi.Helpers.Serializer;

namespace UniversalWebApi.Controllers.BaseControllers
{
    public class SerializeController<T> : Controller where T : class
    {
        protected ISqlRepository Db => (ISqlRepository)HttpContext.RequestServices.GetService(typeof(ISqlRepository));
        protected ISerializeHelper SerializeHelper => (ISerializeHelper)HttpContext.RequestServices.GetService(typeof(ISerializeHelper));

        //protected readonly ISqlRepository Db;
        //protected readonly ISerializeHelper SerializeHelper;

        //public SerializeController(ISqlRepository db, ISerializeHelper serializeHelper)
        //{
        //    Db = db;
        //    SerializeHelper = serializeHelper;
        //}

        [HttpGet]
        public async Task<byte[]> Get()
        {
            var result = await Db.GetAllAsync<T>();
            return SerializeHelper.ToByteArray(result);
        }

        //[HttpGet("get/{id}")]
        //public async Task<byte[]> Get(byte[] byteArray)
        //{
        //    var deserializedObject = (int) SerializeHelper.DeserializeId(byteArray);

        //    var result = await Db.GetAsync<T>(deserializedObject);

        //    return SerializeHelper.ToByteArray(result);
        //}

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] byte[] byteArray)
        {
            try
            {
                var result = SerializeHelper.FromByteArray<T>(byteArray);
                await Db.InsertAsync(result);
                return Ok("Added successfully!");
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] byte[] byteArray)
        {
            try
            {
                var result = SerializeHelper.FromByteArray<T>(byteArray);

                await Db.UpdateAsync(result);
                return Ok("Updated successfully!");
            }
            catch (Exception e)
            {
                return Json(e);
            }
        }

        //[HttpDelete("delete/{id}")]
        //public async Task<IActionResult> Delete(byte[] byteArray)
        //{
        //    try
        //    {
        //        var deserializedObject = (int) SerializeHelper.DeserializeId(byteArray);
        //        await Db.DeleteRowAsync<T>(deserializedObject);
        //        return Ok("Deleted successfully!");
        //    }
        //    catch (Exception e)
        //    {
        //        return Json(e);
        //    }
        //}
    }
}