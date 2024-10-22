using DeliveryApp.Contracts;
using Serilog;
using System.ComponentModel;
using System.Text.Json;

namespace DeliveryApp
{
    public class FileService: IFileService
    {
/// <summary>
        /// Чтение данных о заказах из файла json 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public List<Order> ReadOrders(string filePath)
        {
            var file = File.ReadAllText(filePath);

            var options = new JsonSerializerOptions
            {
                Converters =
                {
                    new DateTimeConverter("yyyy-MM-dd HH:mm:ss")
                }
            };

            var orders = JsonSerializer.Deserialize<List<Order>>(file, options);
            return orders ?? new List<Order>();
        }

        /// <summary>
        /// Фильтрация заказов в соответствии с заданными параметрами 
        /// </summary>
        /// <param name="orders"></param>
        /// <param name="district"></param>
        /// <param name="oneDeliveryTime"></param>
        /// <param name="twoDeliveryTime"></param>
        /// <returns></returns>
        public List<Order> FilterOrders(List<Order> orders, string district, DateTime firstDeliveryDateTime)
        {
            var filterTime = firstDeliveryDateTime.AddMinutes(30);

            return orders.Where(o => o.District == district &&
                                     o.DeliveryTime >= firstDeliveryDateTime &&
                                     o.DeliveryTime <= filterTime).ToList();
        }

        /// <summary>
        /// Запись результата в файл 
        /// </summary>
        /// <param name="resultFilePath"></param>
        /// <param name="orders"></param>
        public void WriteToFile(string resultFilePath, List<Order> orders)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters = { new DateTimeConverter("yyyy-MM-dd HH:mm:ss") }
            };

            var jsonString = JsonSerializer.Serialize(orders, options);

            File.WriteAllText(resultFilePath, jsonString);
        }
    }
}
