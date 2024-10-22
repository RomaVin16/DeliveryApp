namespace DeliveryApp.Models
{
    public class Order
    {
        /// <summary>
        /// Номер заказа 
        /// </summary>
        public Guid OrderNumber { get; set; }

        /// <summary>
        /// Вес заказа в килограммах
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        /// Район заказа 
        /// </summary>
        public string District { get; set; }

        /// <summary>
        /// Время доставки заказа 
        /// </summary>
        public DateTime DeliveryTime { get; set; }
    }
}
