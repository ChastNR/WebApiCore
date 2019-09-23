using System;
using System.Threading.Tasks;
using UniversalWebApi.Helpers.Serializer;
using Microsoft.AspNetCore.Mvc;
using SqlRepository.Interfaces;

namespace UniversalWebApi.Controllers
{
    public class SerializeController<T> : Controller where T : class
    {
        protected readonly IDataRepository Db;
        protected readonly ISerializeHelper SerializeHelper;

        public SerializeController(IDataRepository db, ISerializeHelper serializeHelper)
        {
            Db = db;
            SerializeHelper = serializeHelper;
        }

        [HttpGet("get")]
        public virtual async Task<byte[]> Get()
        {
            var result = await Db.GetAllAsync<T>();
            return SerializeHelper.SerializeObject(result);
        }

        [HttpGet("get/{id}")]
        public virtual async Task<byte[]> Get(byte[] byteArray)
        {
            var deserializedObject = (int) SerializeHelper.DeserializeId(byteArray);

            var result = await Db.GetAsync<T>(deserializedObject);

            return SerializeHelper.SerializeObject(result);
        }

        [HttpPost("add")]
        public virtual async Task<IActionResult> Add([FromBody] byte[] byteArray)
        {
            try
            {
                var result = SerializeHelper.DeserializeObject<T>(byteArray);
                await Db.InsertAsync(result);
                return Ok("Added successfully!");
            }
            catch (Exception e)
            {
                return Json(e);
            }
        }

        [HttpPut("update")]
        public virtual async Task<IActionResult> Update([FromBody] byte[] byteArray)
        {
            try
            {
                var result = SerializeHelper.DeserializeObject<T>(byteArray);

                await Db.UpdateAsync(result);
                return Ok("Updated successfully!");
            }
            catch (Exception e)
            {
                return Json(e);
            }
        }

        [HttpDelete("delete/{id}")]
        public virtual async Task<IActionResult> Delete(byte[] byteArray)
        {
            try
            {
                var deserializedObject = (int) SerializeHelper.DeserializeId(byteArray);
                await Db.DeleteRowAsync<T>(deserializedObject);
                return Ok("Deleted successfully!");
            }
            catch (Exception e)
            {
                return Json(e);
            }
        }
    }
}