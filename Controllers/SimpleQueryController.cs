using Microsoft.AspNetCore.Mvc;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SimpleQueryController : ControllerBase
    {
        private readonly ILogger<QueryController> _logger;
        private readonly SimpleQueryService simpleQuery = new();
        private readonly int[] degrees = Enumerable.Range(1, 100).Select(x => x * 5000).ToArray();
        public SimpleQueryController(ILogger<QueryController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("RunMultiple")]
        public void RunMultiple()
        {
            simpleQuery.RunMultiple(degrees);
        }

    }
}
