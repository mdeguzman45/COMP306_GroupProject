using System;
using System.Collections.Generic;

#nullable disable

namespace StockApp.Models
{
    public partial class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public decimal? Available { get; set; }
        public decimal? Allocated { get; set; }
    }
}
