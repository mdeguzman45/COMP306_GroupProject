using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockAPI.Models;
using StockAPI.Services;
using StockAPI.DTOs;
using AutoMapper;

namespace StockAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTradesController : ControllerBase
    {
        private IStockRepository _stockRepository;
        private readonly IMapper _mapper;

        public UserTradesController(IStockRepository stockRepository, IMapper mapper)
        {
            _stockRepository = stockRepository;
            _mapper = mapper;
        }

        // GET: api/open_trades
        [HttpGet]
        [Route("/api/open_trades/{username}")]
        public async Task<ActionResult<IEnumerable<UserTrade>>> GetUserOpenTrades(string username)
        {
            var userOpenTrades = await _stockRepository.GetUserOpenTrades(username);
            var results = _mapper.Map<IEnumerable<UserTradeDTO>>(userOpenTrades);

            return Ok(results);
        }

        // GET: api/close_trades
        [HttpGet]
        [Route("/api/close_trades/{username}")]
        public async Task<ActionResult<IEnumerable<UserTrade>>> GetUserCloseTrades(string username)
        {
            var userCloseTrades = await _stockRepository.GetUserCloseTrades(username);
            var results = _mapper.Map<IEnumerable<UserTradeDTO>>(userCloseTrades);

            return Ok(results);
        }

        // GET: api/trade/{id}
        [HttpGet]
        public async Task<ActionResult<UserTrade>> GetTrade(int id)
        {
            var userTrade = await _stockRepository.GetTrade(id);

            if (userTrade == null) return NotFound();

            var results = _mapper.Map<UserTradeDTO>(userTrade);

            return Ok(results);
        }

        // PUT: api/UserTrades/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserTrade(int id, [FromBody] UserTradeForUpdateDTO userTrade)
        {
            if (userTrade == null)
            {
                return BadRequest();
            }

            var oldUserTradeEnity = await _stockRepository.GetTrade(id);

            if (oldUserTradeEnity == null) return NotFound();

            _mapper.Map(userTrade, oldUserTradeEnity);

            if (!await _stockRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();
        }

        // POST: api/UserTrades
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("/api/createtrade")]
        //public async Task<ActionResult<UserTrade>> PostUserTrade(UserTrade userTrade)
        public async Task<ActionResult<UserTrade>> PostUserTrade([FromBody] UserTradeForCreationDTO userTrade)
        {
            if (userTrade == null)
            {
                return BadRequest();
            }

            if (await _stockRepository.TradeIdExists((int)userTrade.TradeId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var finalCreateUserTrade = _mapper.Map<UserTrade>(userTrade);
            _stockRepository.CreateUserTrade(finalCreateUserTrade);

            if (!await _stockRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return CreatedAtAction("GetTrade", new { id = finalCreateUserTrade.TradeId });
        }

        // DELETE: api/UserTrades/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserTrade(int id)
        {
            var userTrade = await _stockRepository.GetTrade(id);

            if (userTrade == null) return NotFound();
            _stockRepository.DeleteUserTrade(userTrade);

            if (!await _stockRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();
        }
    }
}
