using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApi.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal TotalPrice { get; set; }
        public int UserId { get; set; }
        public int AddressId { get; set; }
        public DateTime InitialDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public bool IsDelivery { get; set; }
}
}
