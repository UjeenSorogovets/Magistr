namespace WebApplication.Services
{
    public class SimpleQueryService
    {
        private readonly CommonService commonService;
        public SimpleQueryService()
        {
            commonService = new CommonService();
        }

        public void Run(IEnumerable<int> numbers)
        {
            var startTime = System.Diagnostics.Stopwatch.StartNew();
            Console.WriteLine("Start Simple");
            commonService.Primes(numbers);
            startTime.Stop();
            Console.WriteLine($"Simple - {startTime.ElapsedMilliseconds}ms" + Environment.NewLine);
        }
    }
}
