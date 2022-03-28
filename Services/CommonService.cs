namespace WebApplication.Services
{
    public class CommonService
    {
        public CommonService()
        {

        }

        public void Primes(IEnumerable<int> numbers)
        {
            var simpleQuery =
                from n in numbers
                where Enumerable.Range(2, (int)Math.Sqrt(n)).All(i => n % i > 0)
                select n;
            var primes = simpleQuery.ToArray();
        }
    }
}
