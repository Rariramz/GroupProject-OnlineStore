using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Entities
{
    public class Order
    {
        public int ID { get; set; }
        public string? Description { get; set; }
        public decimal TotalPrice { get; set; }
        public string UserID { get; set; } = "";
        public int AddressID { get; set; }
        public DateTime InitialDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public bool IsDelivery { get; set; }

        public User? User {  get; set; }
        public Address? Address {  get; set; }
        public ICollection<UserItem>? UserItems { get; set;}
    }
}
