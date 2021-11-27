namespace StockAPI.DTOs
{
    public class StockWithoutTradeDTO
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public decimal? LastPrice { get; set; }
    }
}
