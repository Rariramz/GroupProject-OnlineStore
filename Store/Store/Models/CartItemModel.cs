namespace Store.Models
{
    public class CartItemModel
    {
        public string UserID { get; set; } = "";
        public int ItemID { get; set; }
        public int Count { get; set; }
    }
}
