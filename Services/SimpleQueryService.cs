namespace WebApplication.Services
{
    public class SimpleQueryService
    {
        private readonly CommonService commonService;
        private readonly LoggerService logger = new();
        public SimpleQueryService()
        {
            commonService = new CommonService();
        }

        public void Run(IEnumerable<int> numbers)
        {
            var startTime = System.Diagnostics.Stopwatch.StartNew();
            logger.Log("Start Simple");
            commonService.Primes(numbers);
            startTime.Stop();
            logger.Log($"Simple - {startTime.ElapsedMilliseconds}ms" + Environment.NewLine);
        }
    }
}
