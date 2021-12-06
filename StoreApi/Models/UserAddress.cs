using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApi.Models
{
    public class UserAddress
    {
        public int Id { get; set; }
        public int AdressId { get; set; }
        public int UserId { get; set; }
    }
}
