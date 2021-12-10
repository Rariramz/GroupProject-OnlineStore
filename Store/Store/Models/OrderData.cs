namespace Store.Models
{
    public class OrderData
    {
        public List<OrderItemData> ItemDatas { get; set; } = new List<OrderItemData>();
        public decimal TotalPrice { get; set; } 
        public DateTime InitialDate { get; set; }
        public AddressData AddressData { get; set; } = new AddressData();
    }
}
