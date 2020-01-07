using System;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace UniversalWebApi.Controllers
{
    [Authorize]
    [Route("v1/[controller]")]
    public abstract class ApiBaseController<TController, TService> : ControllerBase where TService: class
    {
        protected readonly ILogger<TController> _logger;
        protected readonly TService _service;

        protected ApiBaseController(ILogger<TController> logger, TService service)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }
    }
}