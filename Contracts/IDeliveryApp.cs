using DeliveryApp.Models;

namespace DeliveryApp.Contracts
{
    public interface IDeliveryApp
    {
        public List<Order> Process(string district, DateTime firstDeliveryDateTime);
    }
}
