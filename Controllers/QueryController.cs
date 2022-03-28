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
            foreach (var degree in degrees)
            {
                IEnumerable<int> numbers = Enumerable.Range(3, degree);
                simpleQuery.Run(numbers);
            }
        }

        [HttpGet]
        [Route("RunParallel")]
        public void RunParallel()
        {
            foreach (var degree in degrees)
            {
                IEnumerable<int> numbers = Enumerable.Range(3, degree);
                parallelQuery.Run(numbers);
            }
        }

        [HttpGet]
        [Route("RunOneAsyncQuery")]
        public async Task RunOneAsyncQuery()
        {
            foreach (var degree in degrees)
            {
                IEnumerable<int> numbers = Enumerable.Range(3, degree);
                await asyncQuery.OneAsyncQuery(numbers);
            }
        }

        [HttpGet]
        [Route("RunWhenAllAsyncQuery")]
        public async Task RunWhenAllAsyncQuery()
        {
            foreach (var degree in degrees)
            {
                IEnumerable<int> numbers = Enumerable.Range(3, degree);
                await asyncQuery.WhenAllAsyncQuery(numbers);
            }
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