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
        private static readonly int asyncCount = 2;
        private readonly SimpleQuery simpleQuery = new();
        public QueryController(ILogger<QueryController> logger)
        {
            _logger = logger;
            simpleQuery = new SimpleQuery();
        }

        static void Primes()
        {
            var simpleQuery =
                from n in numbers
                where Enumerable.Range(2, (int)Math.Sqrt(n)).All(i => n % i > 0)
                select n;
            var primes = simpleQuery.ToArray();
        }

        static async Task AsyncTask(int n)
        {
            await Task.Run(() =>
            {
                var startTime = System.Diagnostics.Stopwatch.StartNew();
                Primes();
                startTime.Stop();
                Console.WriteLine($"{n} Async - {startTime.ElapsedMilliseconds}ms");
            });
        }

        static async Task AsyncQuery()
        {
            for (int i = 0; i < asyncCount; i++)
            {
                await AsyncTask(i);
            }
        }

        static void SimpleQuery()
        {
            var startTime = System.Diagnostics.Stopwatch.StartNew();
            Console.WriteLine("Start Simple");
            Primes();
            startTime.Stop();
            Console.WriteLine($"Simple - {startTime.ElapsedMilliseconds}ms" + Environment.NewLine);
        }

        static void ParallelQuery()
        {
            var startTime = System.Diagnostics.Stopwatch.StartNew();
            Console.WriteLine("Start Parallel");
            var parallelQuery =
               from n in numbers.AsParallel()
               where Enumerable.Range(2, (int)Math.Sqrt(n)).All(i => n % i > 0)
               select n;
            int[] primes = parallelQuery.ToArray();
            startTime.Stop();
            Console.WriteLine($"Parallel - {startTime.ElapsedMilliseconds}ms" + Environment.NewLine);
        }

        static async Task OneAsyncQuery()
        {
            var startTime = System.Diagnostics.Stopwatch.StartNew();  
            Console.WriteLine("Start ASYNC Single results");
            await AsyncQuery();
            startTime.Stop();
            Console.WriteLine($"Final Single Async - {startTime.ElapsedMilliseconds}ms" + Environment.NewLine);
        }

        static async Task WhenAllAsyncQuery()
        {
            Console.WriteLine("Start WhenAll results");
            Console.WriteLine("Start WhenAll results");
            var startTime = System.Diagnostics.Stopwatch.StartNew();
            await Task.WhenAll(AsyncQuery(), AsyncQuery());
            startTime.Stop();
            Console.WriteLine($"Final WhenAll result \nAsync - {startTime.ElapsedMilliseconds}ms" + Environment.NewLine);
        }

        [HttpGet]
        [Route("Test")]
        public async Task Test()
        {
            simpleQuery.Run(numbers);
            ParallelQuery();
            await OneAsyncQuery();
            await WhenAllAsyncQuery();
        }
    }
}