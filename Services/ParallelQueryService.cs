namespace WebApplication.Services
{
    public class ParallelQueryService
    {
        private readonly CommonService commonService;
        private readonly LoggerService logger = new();
        public ParallelQueryService()
        {
            commonService = new CommonService();
        }

        public void Run(IEnumerable<int> numbers)
        {
            var startTime = System.Diagnostics.Stopwatch.StartNew();
            logger.Log("Start Parallel");
            var parallelQuery =
               from n in numbers.AsParallel()
               where Enumerable.Range(2, (int)Math.Sqrt(n)).All(i => n % i > 0)
               select n;
            int[] primes = parallelQuery.ToArray();
            startTime.Stop();
            logger.Log($"Parallel - {startTime.ElapsedMilliseconds}ms" + Environment.NewLine);
        }
    }
}
