using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockAPI.Models;
using StockAPI.Services;
using StockAPI.DTOs;

namespace StockAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private IStockRepository _stockRepository;
        private readonly IMapper _mapper;

        public StocksController(IStockRepository stockRepository, IMapper mapper)
        {
            _stockRepository = stockRepository;
            _mapper = mapper;
        }

        // GET: api/Stocks
        [HttpGet]
        [Route("/api/stocks")]
        public async Task<ActionResult<IEnumerable<Stock>>> GetStocks()
        {
            var stockInfoes = await _stockRepository.GetStockInfoes();
            var results = _mapper.Map<IEnumerable<StockWithoutTradeDTO>>(stockInfoes);

            return Ok(results);
        }

        // GET: api/Stocks/5
        // [HttpGet("{id}")]
        [HttpGet("/api/stock/{symbol}")]
        public async Task<ActionResult<Stock>> GetStock(string symbol)
        {
            var stock = await _stockRepository.GetStockBySymbol(symbol);

            if (stock == null)
            {
                return NotFound();
            }

            var stockWithoutUserTrade = _mapper.Map<StockWithoutTradeDTO>(stock);
            return Ok(stockWithoutUserTrade);
        }

        //// PUT: api/Stocks/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutStock(string id, Stock stock)
        //{
        //    if (id != stock.Symbol)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(stock).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!StockExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Stocks
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Stock>> PostStock(Stock stock)
        //{
        //    _context.Stocks.Add(stock);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (StockExists(stock.Symbol))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetStock", new { id = stock.Symbol }, stock);
        //}

        //// DELETE: api/Stocks/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteStock(string id)
        //{
        //    var stock = await _context.Stocks.FindAsync(id);
        //    if (stock == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Stocks.Remove(stock);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool StockExists(string id)
        //{
        //    return _context.Stocks.Any(e => e.Symbol == id);
        //}
    }
}
