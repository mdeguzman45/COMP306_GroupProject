using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StockClient.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace StockClient.Controllers
{
    public class UserTradesController : Controller
    {
        //Hosted web API REST Service base url
        string Baseurl = "http://stockapi-dev.us-east-1.elasticbeanstalk.com/";

        private readonly USERLOGINContext _loginContext;

        public UserTradesController(USERLOGINContext loginContext)
        {
            _loginContext = loginContext;
        }

        public async Task<ActionResult> Index()
        {
            List<UserTrade> UserTradeInfo = new List<UserTrade>();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();

                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var loggedInUser = HttpContext.Session.GetString("LoggedInUser");
                Console.WriteLine("Logged in user: " + loggedInUser);
                string user = loggedInUser;
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.GetAsync("api/open_trades/" + user);

                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var UserTradeResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list
                    UserTradeInfo = JsonConvert.DeserializeObject<List<UserTrade>>(UserTradeResponse);
                }

                //returning the employee list to view
                return View(UserTradeInfo);
            }
        }

        public async Task<ActionResult> History()
        {
            List<UserTrade> UserTradeInfo = new List<UserTrade>();
            //UserTrade UserTradeInfo = new UserTrade();

            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();

                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var loggedInUser = HttpContext.Session.GetString("LoggedInUser");
                Console.WriteLine("Logged in user: " + loggedInUser);
                string user = loggedInUser;
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.GetAsync("api/close_trades/" + user);

                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var UserTradeResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list
                    UserTradeInfo = JsonConvert.DeserializeObject<List<UserTrade>>(UserTradeResponse);
                    //UserTradeInfo = JsonConvert.DeserializeObject<UserTrade>(UserTradeResponse);
                }

                //returning the employee list to view
                return View(UserTradeInfo);
            }
        }

        public async Task<IActionResult> OpenTrade(string symbol)
        {
            // Get the stock information
            Stock stock = await GetStockDetails(symbol);
            ViewBag.Stock = stock;

            // Get the user details
            var loggedInUser = HttpContext.Session.GetString("LoggedInUser");
            User userDetails = _loginContext.Customers.Where(e => e.Username == loggedInUser).FirstOrDefault();

            if (userDetails == null)
            {
                return NotFound();
            }
            ViewBag.AvailableFunds = userDetails.Available;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OpenTrade(IFormCollection formCollection)
        // public IActionResult OpenTrade(IFormCollection formCollection)
        {
            var entryPrice = formCollection["shareprice"];
            var symbol = formCollection["symbol"];
            var unit = formCollection["unit"];
            var amount = formCollection["amount"];
            var position = formCollection["position"];
            string tradeOpenDate = DateTime.Now.ToString("MM/dd/yyyy");
            long tradeId = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            var loggedInUser = HttpContext.Session.GetString("LoggedInUser");

            var parsedUnit = float.Parse(unit);
            var parsedAmount = float.Parse(amount);
            var parsedEntryPrice = float.Parse(entryPrice);

            Random generator = new Random();

            Console.WriteLine($"entry price: {entryPrice}\nsymbol: {symbol}\nunit: {unit}\namount: {amount}");
            // Create the UserTrade Object for Post Request
            UserTrade userTrade = new UserTrade
                {
                 
                    TradeId = generator.Next(0, 1000000),

            //TradeId = DateTimeOffset.Now.ToUnixTimeMilliseconds(),
            //TradeId = 10, // remove this when republished with long TradeId
            // TradeId = tradeId,
                    Username = loggedInUser,
                    Symbol = symbol,
                    Units = parsedUnit,
                    Position = position,
                    TradeOpenDate = tradeOpenDate,
                    TradeCloseDate = "",
                    EntryPrice = parsedEntryPrice,
                    Amount = parsedAmount
            };

            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();

                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var json = JsonConvert.SerializeObject(userTrade);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                //Sending request to find web api REST service resource create user trade
                HttpResponseMessage Res = await client.PostAsync("/api/createtrade", content);

                Console.WriteLine($"status from POST {Res.StatusCode}");

                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "UserTrades");
                }
            }

            // if we got an error return not found
            return NotFound();
            
        }


        
        public async Task<ActionResult> CloseTrade(long tradeId)
        {
            //List<UserTrade> UserTradeInfo = new List<UserTrade>();
            UserTrade UserTradeInfo = new UserTrade();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();

                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var loggedInUser = HttpContext.Session.GetString("LoggedInUser");
                Console.WriteLine("Logged in user: " + loggedInUser);
                string user = loggedInUser;
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.GetAsync("api/UserTrades?id=" + tradeId);

                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var UserTradeResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list
                    //UserTradeInfo = JsonConvert.DeserializeObject<List<UserTrade>>(UserTradeResponse);
                    UserTradeInfo = JsonConvert.DeserializeObject<UserTrade>(UserTradeResponse);
                    Console.WriteLine("CloseTrade - Trade ID:" + UserTradeInfo.TradeId + "\nClosed By: " + UserTradeInfo.Username);

                    UserTrade updatedUserTrade = new UserTrade
                    {
                        TradeId = UserTradeInfo.TradeId,
                        Username = loggedInUser,
                        Symbol = UserTradeInfo.Symbol,
                        Units = UserTradeInfo.Units,
                        Position = UserTradeInfo.Position,
                        TradeOpenDate = UserTradeInfo.TradeOpenDate,
                        TradeCloseDate = DateTime.Now.ToString("MM/dd/yyyy"),
                        EntryPrice = UserTradeInfo.EntryPrice,
                        Amount = UserTradeInfo.Amount
                    };

                    string id = tradeId.ToString();
                    Console.WriteLine("tradeId is: " + id);
                    //PUT
                    var putTask = client.PutAsJsonAsync<UserTrade>("api/UserTrades/" + updatedUserTrade.TradeId.ToString(), updatedUserTrade);
                    putTask.Wait();

                    var result = putTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("History");
                    }
                }

                //returning the employee list to view
                return RedirectToAction("History");

            }
        }
        

        public async Task<ActionResult> Delete(long tradeId)
        {

            Console.WriteLine("UserTradesController - Delete");
            UserTrade UserTradeInfo = new UserTrade();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();

                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                //Sending request to find web api REST service resource Delete UserTrade using HttpClient
                HttpResponseMessage Res = await client.DeleteAsync("api/UserTrades/" + tradeId.ToString());

                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var UserTradeResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list
                    UserTradeInfo = JsonConvert.DeserializeObject<UserTrade>(UserTradeResponse);
                }

                //returning the stock details to view
                //return View(UserTradeInfo);
                return RedirectToAction("History", "UserTrades");
            }
        }

        public async Task<Stock> GetStockDetails(string inputSymbol)
        {
           
            //List<Stock> StockInfo = new List<Stock>();
            Stock StockInfo = new Stock();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();

                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.GetAsync("api/stock/" + inputSymbol);

                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var StockResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list
                    //StockInfo = JsonConvert.DeserializeObject<List<Stock>>(StockResponse);
                    StockInfo = JsonConvert.DeserializeObject<Stock>(StockResponse);
                }
            }

            return StockInfo;
        }
    }
}