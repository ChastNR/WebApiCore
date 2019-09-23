using System;
using System.Threading.Tasks;
using UniversalWebApi.Schedulers;
using Microsoft.AspNetCore.Mvc;

namespace UniversalWebApi.Controllers
{
    [Route("api/[controller]")]
    public class EmailController : Controller
    {
        private readonly IEmailScheduler _scheduler;

        public EmailController(IEmailScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        [HttpPost("startScheduler")]
        public async Task<IActionResult> StartScheduler([FromBody] EmailModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Subject) || string.IsNullOrWhiteSpace(model.Message))
                return Json("Please add subject and message for your request");

            try
            {
                await _scheduler.Start(model.Subject, model.Message);
                return Ok("Task started");
            }
            catch (Exception e)
            {
                return Json(e);
            }
        }
    }

    public class EmailModel
    {
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}