﻿namespace Store.Models
{
    public class ItemData
    {
        public int ID { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public float Price { get; set; }
        public int CategoryID { get; set; }
    }
}
