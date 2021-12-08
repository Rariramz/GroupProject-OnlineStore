namespace Store.Entities
{
    public class Category
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? ParentID { get; set; }
        public string? Image { get; set; }
        public string? InsideImage { get; set; }

        public ICollection<Category>? ChildCategories { get; set; }
        public ICollection<Item>? ChildItems { get; set; }
        public Category? Parent { get; set; }
    }
}
