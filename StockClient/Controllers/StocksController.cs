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
    public class StocksController : Controller
    {

        //Hosted web API REST Service base url
        string Baseurl = "http://stockapi-dev.us-east-1.elasticbeanstalk.com/";

        public async Task<ActionResult> Index()
        {
            List<Stock> StockInfo = new List<Stock>();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();

                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.GetAsync("api/stocks");

                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var StockResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list
                    StockInfo = JsonConvert.DeserializeObject<List<Stock>>(StockResponse);
                }

                //returning the employee list to view
                return View(StockInfo);
            }
        }


        public async Task<ActionResult> Details(string inputSymbol)
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

                //returning the stock details to view
                return View(StockInfo);
            }
        }
    }
}