﻿

using CoffeeManagement.Model;

namespace CoffeeManagement.RequestModel.Account
{
    public class AccountRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
    }
}
