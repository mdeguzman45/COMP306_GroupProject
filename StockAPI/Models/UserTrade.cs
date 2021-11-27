using System;
using System.Collections.Generic;

#nullable disable

namespace StockAPI.Models
{
    public partial class UserTrade
    {
        public int TradeId { get; set; }
        public string Username { get; set; }
        public string Symbol { get; set; }
        public decimal? Units { get; set; }
        public string Position { get; set; }
        public string TradeOpenDate { get; set; }
        public string TradeCloseDate { get; set; }
        public decimal? EntryPrice { get; set; }
        public decimal? Amount { get; set; }

        public virtual Stock SymbolNavigation { get; set; }
    }
}
