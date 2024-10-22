using DeliveryApp.Models;

namespace DeliveryApp.Contracts
{
    public interface IFileService
    {
        List<Order> ReadOrders(string filePath);
        List<Order> FilterOrders(List<Order> orders, string district, DateTime firstDeliveryDateTime);
        void WriteToFile(string filePath, List<Order> orders);
    }
}
