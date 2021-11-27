using System;
using System.Collections.Generic;

#nullable disable

namespace StockAPI.Models
{
    public partial class Stock
    {
        public Stock()
        {
            UserTrades = new HashSet<UserTrade>();
        }

        public string Symbol { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public decimal? LastPrice { get; set; }

        public virtual ICollection<UserTrade> UserTrades { get; set; }
    }
}
