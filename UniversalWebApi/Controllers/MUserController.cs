using Microsoft.AspNetCore.Mvc;
using UniversalWebApi.Controllers.BaseControllers;
using UniversalWebApi.Models;

namespace UniversalWebApi.Controllers
{
    [Route("api/[controller]")]
    public class MUserController : MongoDbController<MUser>
    {
        //public MUserController(IMongoRepository db, IExceptionManager exceptionManager) : base(db, exceptionManager)
        //{
        //}
    }
}