using DeliveryApp.Contracts;
using Serilog;

namespace DeliveryApp
{
    public class DeliveryApp: IDeliveryApp
    {
        private readonly string inputFilePath;
        private readonly string resultFilePath;

        private readonly IFileService _fileService;

        public DeliveryApp(IFileService fileService, IConfiguration configuration, IServiceProvider services)
        {
            _fileService = fileService;
            inputFilePath = configuration["OrderFilePath"];
            resultFilePath = configuration["ResultFilePath"];
        }

        /// <summary>
        /// Обработка списка заказов 
        /// </summary>
        /// <param name="district"></param>
        /// <param name="deliveryDateTime"></param>
        /// <param name="twoDeliveryTime"></param>
        /// <returns></returns>
        public List<Order> Process(string district, DateTime firstDeliveryDateTime)
        {
            var filteredOrders = new List<Order>();

            try
            {
                var orders = _fileService.ReadOrders(inputFilePath);
                Log.Information("Список заказов успешно прочитан. Количество заказов: {OrderCount}", orders.Count);

                filteredOrders = _fileService.FilterOrders(orders, district, firstDeliveryDateTime);
                Log.Information("Заказы успешно отфильтрованы. Количество заказов, соответствующих заданному фильтру: {OrderCount}", orders.Count);

                _fileService.WriteToFile(resultFilePath, filteredOrders);
                Log.Information("Заказы успешно записаны в результирующий файл.");
            }
            catch (Exception e)
            {
                Log.Error(e, "При обработке файла произошла ошибка. Файл: {FilePath}, Район: {District}", inputFilePath,  district);
            }

            return filteredOrders;
        }
    }
}
