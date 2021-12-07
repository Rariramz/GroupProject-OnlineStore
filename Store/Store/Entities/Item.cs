namespace Store.Entities
{
    public class Item
    {
        public int ID {  get; set; }
        public string? Name {  get; set; }
        public decimal Price { get; set; }
        public string? Description {  get; set; }
        public string? Image { get; set; }
        public int CategoryID { get; set; }


        public Category? Category { get; set; }

        public decimal GetDiscountPrice(int discount)
        {
            return Price - Price * (discount / 100);
        }
    }
}
