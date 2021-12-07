using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public decimal TotalPrice { get; set; }
        public int UserId { get; set; }
        public int AddressId { get; set; }

        [DataType(DataType.Date)]
        public DateTime InitialDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime DeliveryDate { get; set; }

        public bool IsDelivery { get; set; }
}
}
