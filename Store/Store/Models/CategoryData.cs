using Store.Entities;

namespace Store.Models
{
    public class CategoryData
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? ParentID { get; set; }
        public int ImageID { get; set; }
        public int InsideImageID { get; set; }
        public List<Item> Items { get; set; } = new List<Item>();
        public List<int> ChildCategoriesId {  get; set; } = new List<int>();
    }
}
