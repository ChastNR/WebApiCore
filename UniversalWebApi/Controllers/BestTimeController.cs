using Microsoft.AspNetCore.Mvc;
using SqlRepository.Interfaces;
using UniversalWebApi.Controllers.BaseControllers;
using UniversalWebApi.Helpers.Serializer;
using UniversalWebApi.Models;

namespace UniversalWebApi.Controllers
{
    [Route("api/[controller]")]
    public class BestTimeController : SerializeController<BestTime>
    {
        public BestTimeController(IDataRepository db, ISerializeHelper serializeHelper) : base(db, serializeHelper)
        {
        }
    }
}