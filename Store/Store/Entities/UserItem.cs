using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Entities
{
    public class UserItem
    {
        public int ID { get; set; }
        public string UserID { get; set; } = "";
        public int ItemID { get; set; }
        public int OrderID { get; set; }
        public int Count { get; set; }

        public User? User { get; set; }
        public Item? Item { get; set; }
        public Order? Order { get; set; }
    }
}
