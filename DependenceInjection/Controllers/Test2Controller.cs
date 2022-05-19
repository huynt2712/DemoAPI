using DependenceInjection.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DependenceInjection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Test2Controller : ControllerBase
    {
        private ISingletonService _singletonService;
        private ITransientService _transientService;

        public Test2Controller(ISingletonService singletonService,
            ITransientService transientService)
        {
            _singletonService = singletonService;
            _transientService = transientService;
        }

        [HttpGet("test2")]
        public IActionResult Test2()
        {
            //var value = _singletonService.GetGuid();
            var value = _transientService.GetGuid();
            return Ok(value);
        }
    }
}
