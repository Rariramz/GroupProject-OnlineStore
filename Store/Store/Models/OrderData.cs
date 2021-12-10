namespace Store.Models
{
    public class OrderData
    {
        public int ID { get; set; }
        public string Description { get; set; } = "";
        public decimal TotalPrice { get; set; }
        public string Address { get; set; } = "";
        public DateTime InitialDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public bool IsDelivery { get; set; }
    }
}
