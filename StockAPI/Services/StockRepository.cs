using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockAPI.Models;
using System;

namespace StockAPI.Services
{
    public class StockRepository : IStockRepository
    {
        private STOCKSContext _context;

        public StockRepository(STOCKSContext context)
        {
            _context = context;
        }

        public async Task<bool> StockExists(string symbol)
        {
            return await _context.Stocks.AnyAsync<Stock>(s => s.Symbol == symbol);
        }

        public async Task<bool> TradeIdExists(int id)
        {
            return await _context.UserTrades.AnyAsync<UserTrade>(u => u.TradeId == id);
        }

        public async Task<IEnumerable<Stock>> GetStockInfoes()
        {
            var result = _context.Stocks.OrderBy(s => s.Symbol);
            return await result.ToListAsync();
        }
        public async Task<Stock> GetStockBySymbol(string symbol)
        {
            IQueryable<Stock> result;
            result = _context.Stocks.Where(s => s.Symbol == symbol);

            return await result.FirstOrDefaultAsync();
        }

        // Get trades of the user that are open
        public async Task<IEnumerable<UserTrade>> GetUserOpenTrades(string username)
        {
            var userTrades = _context.UserTrades.Where(u => u.Username.Equals(username) && u.TradeCloseDate == "");
            return await userTrades.ToListAsync();
        }

        // Get trades of the user that are close
        public async Task<IEnumerable<UserTrade>> GetUserCloseTrades(string username)
        {
            var userTrades = _context.UserTrades.Where(u => u.Username.Equals(username) && u.TradeCloseDate != "");
            return await userTrades.ToListAsync();
        }

        // Get trade by id
        public async Task<UserTrade> GetTrade(int id)
        {
            var userTrade = _context.UserTrades.Where(u => u.TradeId == id);
            return await userTrade.FirstOrDefaultAsync();
        }

        // Create the trade for the user
        public void CreateUserTrade(UserTrade userTrade)
        {
            _context.UserTrades.Add(userTrade);
        }

        // Delete a trade
        public void DeleteUserTrade(UserTrade userTrade)
        {
            _context.UserTrades.Remove(userTrade);
        }

        public async Task<bool> Save()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
