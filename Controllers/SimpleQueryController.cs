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
        private readonly int[] degrees = { 10, 100, 1000, 10000, 100_000, 500_000, 1_000_000 };
        public SimpleQueryController(ILogger<QueryController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("RunSimple")]
        public void RunSimple()
        {
            simpleQuery.RunMultiple(degrees);
        }

    }
}
