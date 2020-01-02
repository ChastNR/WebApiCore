using System;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace UniversalWebApi.Controllers.BaseControllers
{
    
    [Route("api/[controller]")]
    public abstract class BaseController<TController, TService> : Controller where TService: class
    {
        protected readonly ILogger<TController> _logger;
        protected readonly TService _service;

        protected BaseController(ILogger<TController> logger, TService service)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }
    }
}