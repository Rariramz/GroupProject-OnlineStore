using Store.Entities;

namespace Store.Models
{
    public class CategoryData
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? ParentID { get; set; }
        public int InsideImageID { get; set; }
        public List<int> Items { get; set; } = new List<int>();
        public List<int> ChildCategoriesId {  get; set; } = new List<int>();
    }
}
