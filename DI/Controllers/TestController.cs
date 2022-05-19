using DI.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private ISingletonService singletonService;
        private IScopedService scopedService;
        private IScopedService scopedService2;

        public TestController(ISingletonService singletonService, IScopedService scopedService,
            IScopedService scopedService2 )
        {
            this.singletonService = singletonService;
            this.scopedService = scopedService;
            this.scopedService2 = scopedService2;
        }

        [HttpGet]
        public IActionResult Test()
        {
            //var value = singletonService.GetID();
            var value1 = scopedService.GetID();

            var value2 = scopedService2.GetID();

            return new JsonResult(value1 + " " + value2);
        }

        [HttpGet("test2")]
        public IActionResult Test2()
        {
            //var value = singletonService.GetID();
            var value = scopedService.GetID();
            return new JsonResult(value);
        }
    }
}
