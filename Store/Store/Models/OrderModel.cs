namespace Store.Models
{
    public class OrderModel
    {
        public int ID { get; set; }
        public string Description { get; set; } = "";
        public decimal TotalPrice { get; set; }
        public string UserID { get; set; } = "";
        public int AddressID { get; set; }
        public DateTime InitialDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public bool IsDelivery { get; set; }
    }
}
