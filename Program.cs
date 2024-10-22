using DeliveryApp.Contracts;
using DeliveryApp.Helpers;
using DeliveryApp.Services;
using Serilog;


namespace DeliveryApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var logFilePath = args.Length > 0 ? args[0] : configuration["LogFilePath"];

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .WriteTo.File(logFilePath) 
                .CreateLogger();

            try
            {
                var host = Host.CreateDefaultBuilder(args)
                    .UseSerilog() 
                    .ConfigureServices(ServiceConfigurator.ConfigureServices)
                    .Build();

                RunApp(host.Services);
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Программа завершилась с ошибкой");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static void RunApp(IServiceProvider services)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("Запуск программы");

            var validator = new Validator(services.GetRequiredService<IConfiguration>());
            var deliveryApp = services.GetRequiredService<IDeliveryApp>();

            try
            {
                var district = validator.InputDistrict();
                var firstDeliveryDateTime = validator.InputTheTimeFirstDelivery();

                deliveryApp.Process(district, firstDeliveryDateTime);
            }
            catch (Exception ex)
            {
                logger.LogError(ex,"При работе программы возникла ошибка");
            }
        }
    }
}





