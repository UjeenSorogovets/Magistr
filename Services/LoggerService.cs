namespace WebApplication.Services
{
    public class LoggerService
    {
        private readonly bool isActive = false;
        public LoggerService()
        {

        }

        public void Log(string text)
        {
            if (isActive) Console.WriteLine(text);
        }
    }
}
