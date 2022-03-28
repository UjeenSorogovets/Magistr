namespace WebApplication.Services
{
    public class AsyncQueryService
    {
        private readonly CommonService commonService;
        private readonly LoggerService logger = new();
        private readonly int asyncCount = 2;
        public AsyncQueryService()
        {
            commonService = new CommonService();
        }

        async Task AsyncTask(int n, IEnumerable<int> numbers)
        {
            await Task.Run(() =>
            {
                var startTime = System.Diagnostics.Stopwatch.StartNew();
                commonService.Primes(numbers);
                startTime.Stop();
                logger.Log($"{n} Async - {startTime.ElapsedMilliseconds}ms");
            });
        }

        public async Task Run(IEnumerable<int> numbers)
        {
            for (int i = 0; i < asyncCount; i++)
            {
                await AsyncTask(i, numbers);
            }
        }

        public async Task OneAsyncQuery(IEnumerable<int> numbers)
        {
            var startTime = System.Diagnostics.Stopwatch.StartNew();
            logger.Log("Start ASYNC Single results");
            await Run(numbers);
            startTime.Stop();
            logger.Log($"Final Single Async - {startTime.ElapsedMilliseconds}ms" + Environment.NewLine);
        }

        public async Task WhenAllAsyncQuery(IEnumerable<int> numbers)
        {
            logger.Log("Start WhenAll results");
            logger.Log("Start WhenAll results");
            var startTime = System.Diagnostics.Stopwatch.StartNew();
            await Task.WhenAll(Run(numbers), Run(numbers));
            startTime.Stop();
            logger.Log($"Final WhenAll result \nAsync - {startTime.ElapsedMilliseconds}ms" + Environment.NewLine);
        }
    }
}
