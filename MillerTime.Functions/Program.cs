using Microsoft.Extensions.Hosting;

namespace MillerTime.Functions
{
    public class Program
    {
        public static void Main(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureFunctionsWorkerDefaults()
                .Build()
                .Run();
    }
}

