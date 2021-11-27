using System;
using System.Collections.Generic;


namespace StockApp.Models
{
    public class Stock
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public float LastPrice { get; set; }
    }
}