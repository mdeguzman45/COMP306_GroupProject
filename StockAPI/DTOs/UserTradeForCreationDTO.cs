using System.ComponentModel.DataAnnotations;

namespace StockAPI.DTOs
{
    public class UserTradeForCreationDTO
    {
        public int TradeId { get; set; }
        public string Username { get; set; }
        public string Symbol { get; set; }
        [Required(ErrorMessage = "You should provide how many shares for the trade")]
        public decimal? Units { get; set; }
        [Required(ErrorMessage = "You should provide the position for the trade")]
        public string Position { get; set; }
        public string TradeOpenDate { get; set; }
        public string TradeCloseDate { get; set; }
        public decimal? EntryPrice { get; set; }
        public decimal? Amount { get; set; }
    }
}
