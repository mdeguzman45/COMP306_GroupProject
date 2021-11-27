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
using System.Threading.Tasks;

namespace StockClient.Controllers
{
    public class UserTradesController : Controller
    {
        //Hosted web API REST Service base url
        string Baseurl = "http://stockapi-dev.us-east-1.elasticbeanstalk.com/";

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
                }

                //returning the employee list to view
                return View(UserTradeInfo);
            }
        }
    }
}