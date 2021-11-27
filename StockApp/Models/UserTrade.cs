using System;
using System.Collections.Generic;


namespace StockApp.Models
{
    public class UserTrade
    {
        public int TradeId { get; set; }
        public string Username { get; set; }
        public string Symbol { get; set; }
        public float Units { get; set; }
        public string Position { get; set; }
        public string TradeOpenDate { get; set; }
        public string TradeCloseDate { get; set; }
        public float EntryPrice { get; set; }
        public float Amount { get; set; }
    }
}