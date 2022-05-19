using DependenceInjection.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DependenceInjection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private ISingletonService _singletonService;
        private ITransientService _transientService1;
        private ITransientService _transientService2;
        private IScopedService _scopedService1;
        private IScopedService _scopedService2;

        public TestController(ISingletonService singletonService,
            ITransientService transientService1,
            ITransientService transientService2,
            IScopedService scopedService1,
            IScopedService scopedService2)
        {
            _singletonService = singletonService;
            _transientService1 = transientService1;
            _transientService2 = transientService2;
            _scopedService1 = scopedService1;
            _scopedService2 = scopedService2;
            //this = current class
        }

        [HttpGet]
        public IActionResult Test()
        {
            //var value1 = _singletonService.GetGuid();

            //var value2 = _singletonService.GetGuid();
            var valueScoped1 = _scopedService1.GetGuid();//1
            var valueScoped2 = _scopedService2.GetGuid(); //scope 1 instance the same request

            var valueTranSient1 = _transientService1.GetGuid(); //transient always create new instance
            var valueTranSient2 = _transientService2.GetGuid();

            return Ok(@$"valueScoped1: {valueScoped1}, valueScoped2:{valueScoped2}, 
                        valueTranSient1: {valueTranSient1}, valueTranSient2: {valueTranSient2}");
        }

        [HttpGet("test2")]
        public IActionResult Test2()
        {
            var value = _transientService1.GetGuid();
            return Ok(value);
        }
    }
}
