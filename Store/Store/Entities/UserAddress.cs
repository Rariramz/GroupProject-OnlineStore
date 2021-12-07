using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Entities
{
    public class UserAddress
    {
        public int ID { get; set; }
        public int AddressID { get; set; }
        public string UserID { get; set; } = "";

        public User? User { get; set; }
        public Address? Address { get; set; }
    }
}
