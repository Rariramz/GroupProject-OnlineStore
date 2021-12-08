namespace Store.Models
{
    public class CategoryModel
    {
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public int ParentID { get; set; }
        public IFormFile? Image { get; set; }
        public IFormFile? InsideImage { get; set; }
    }
}
