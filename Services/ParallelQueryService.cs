using Newtonsoft.Json;

namespace WebApplication.Services
{
    public class ParallelQueryService
    {
        private readonly LoggerService logger = new();
        public ParallelQueryService()
        {

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
            File.WriteAllText("../ParallelQuery.txt", output);
            Console.WriteLine(output);
        }
    }
}
