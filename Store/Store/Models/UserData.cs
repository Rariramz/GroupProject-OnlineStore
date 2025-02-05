﻿using Store.Entities;

namespace Store.Models
{
    public class UserData
    {
        public bool Success { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public int Discount { get; set; } = 0;
        public List<AddressData> Addresses { get; set; } = new List<AddressData>();
        public bool IsAdmin { get; set; }
    }
}
