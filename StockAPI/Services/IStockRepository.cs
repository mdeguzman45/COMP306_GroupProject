using System.Collections.Generic;
using System.Threading.Tasks;
using StockAPI.Models;

namespace StockAPI.Services
{
    public interface IStockRepository
    {
        Task<bool>StockExists(string symbol);
        Task<bool> TradeIdExists(int id);
        Task<IEnumerable<Stock>> GetStockInfoes();
        // Task<Stock> GetStockBySymbol(string symbol, bool includeTrades);
        Task<Stock> GetStockBySymbol(string symbol);
        Task<IEnumerable<UserTrade>> GetUserOpenTrades(string username);
        Task<IEnumerable<UserTrade>> GetUserCloseTrades(string username);
        Task<UserTrade> GetTrade(int id);
        void CreateUserTrade(UserTrade userTrade);
        void DeleteUserTrade(UserTrade userTrade);
        Task<bool> Save();
    }
}