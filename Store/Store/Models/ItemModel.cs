namespace Store.Models
{
    public class ItemModel
    {
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public float Price {  get; set; } 
        public IFormFile? Image { get; set; }
        public int CategoryID { get; set; }
    }
}
