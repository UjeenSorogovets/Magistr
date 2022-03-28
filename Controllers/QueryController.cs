using Microsoft.AspNetCore.Mvc;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QueryController : ControllerBase
    {
        private readonly ILogger<QueryController> _logger;
        private static readonly IEnumerable<int> numbers = Enumerable.Range(3, 1000000 - 3);
        private readonly SimpleQuery simpleQuery = new();
        private readonly ParallelQueryService parallelQuery = new();
        private readonly AsyncQuery asyncQuery = new(); 
        public QueryController(ILogger<QueryController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("RunAll")]
        public async Task RunAll()
        {
            simpleQuery.Run(numbers);
            parallelQuery.Run(numbers);
            await asyncQuery.OneAsyncQuery(numbers);
            await asyncQuery.WhenAllAsyncQuery(numbers);
        }
    }
}