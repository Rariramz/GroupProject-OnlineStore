using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Models
{
    public class UserAddress
    {
        public int ID { get; set; }
        public int AdressID { get; set; }
        public int UserID { get; set; }

        public User? User { get; set; }
        public Address? Address { get; set; }
    }
}
