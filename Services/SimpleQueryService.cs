using Newtonsoft.Json;

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

        public void RunMultiple(int[] degrees)
        {
            List<(int, int)> array = new();
            foreach (var degree in degrees)
            {
                var startTime = System.Diagnostics.Stopwatch.StartNew();
                IEnumerable<int> numbers = Enumerable.Range(3, degree);
                Run(numbers);

                int elapsedMilliseconds = unchecked((int)startTime.ElapsedMilliseconds);
                array.Add(new(degree, elapsedMilliseconds));
            }
            
            string output = JsonConvert.SerializeObject(array);
            File.WriteAllText("../SimpleQuery.txt", output);
            Console.WriteLine(output);
        }
    }
}
