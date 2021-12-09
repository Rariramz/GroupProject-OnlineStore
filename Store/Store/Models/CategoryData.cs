using Store.Entities;

namespace Store.Models
{
    public class CategoryData
    {
        public int ID {  get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public int? ParentID { get; set; }
        public int InsideImageID { get; set; }
        public List<int> ItemsIDs { get; set; } = new List<int>();
        public List<int> ChildCategoriesIDs {  get; set; } = new List<int>();
    }
}
