using Microsoft.AspNetCore.Mvc;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParallelQueryController : ControllerBase
    {
        private readonly ILogger<QueryController> _logger;
        private readonly ParallelQueryService parallelQuery = new();
        private readonly int[] degrees = Enumerable.Range(1, 100).Select(x => x * 5000).ToArray();
        public ParallelQueryController(ILogger<QueryController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("RunMultiple")]
        public void RunMultiple()
        {
            parallelQuery.RunMultiple(degrees);

        }

    }
}
