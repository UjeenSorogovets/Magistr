using Microsoft.AspNetCore.Mvc;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QueryController : ControllerBase
    {
        private readonly ILogger<QueryController> _logger;
        private static readonly IEnumerable<int> numbers = Enumerable.Range(3, 1000000);
        private readonly SimpleQueryService simpleQuery = new();
        private readonly ParallelQueryService parallelQuery = new();
        private readonly AsyncQueryService asyncQuery = new(); 
        private readonly int[] degrees = { 10, 100, 1000, 10000, 100_000, 500_000, 1_000_000 };
        public QueryController(ILogger<QueryController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("RunSimple")]
        public void RunSimple()
        {
            simpleQuery.RunMultiple(degrees);
        }

        [HttpGet]
        [Route("RunParallel")]
        public void RunParallel()
        {
            parallelQuery.RunMultiple(degrees);
        }

        [HttpGet]
        [Route("RunOneAsyncQuery")]
        public async Task RunOneAsyncQuery()
        {
            await asyncQuery.OneAsyncQueryMultiple(degrees);
        }

        [HttpGet]
        [Route("RunWhenAllAsyncQuery")]
        public async Task RunWhenAllAsyncQuery()
        {
            await asyncQuery.WhenAllAsyncQueryMultiple(degrees);
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