namespace Store.Models
{
    public class OrderItemData
    {
        public ItemData ItemData { get; set; } = new ItemData();
        public int Count { get; set; }
    }
}
