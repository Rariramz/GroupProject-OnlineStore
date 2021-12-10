namespace Store.Entities
{
    public class Category
    {
        public int ID { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public int? ParentID { get; set; }
        public int ImageID { get; set; }
        public int InsideImageID { get; set; }
    }
}
