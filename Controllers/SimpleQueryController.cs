using Microsoft.AspNetCore.Mvc;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SimpleQueryController : ControllerBase
    {
        private readonly ILogger<QueryController> _logger;
        private static readonly IEnumerable<int> numbers = Enumerable.Range(3, 1000000);
        private readonly SimpleQueryService simpleQuery = new();
        private readonly int[] degrees = { 10000, 20000, 50000, 100_000, 300_000, 500_000, 1_000_000 };
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
