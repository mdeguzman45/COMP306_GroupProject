using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StockApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace StockApp
{
    public class Program
    {
        /*static HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();
            await RunAsync();
        }

        static async Task RunAsync()
        {
            client.BaseAddress = new Uri("https://localhost:49159/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                string json;
                HttpResponseMessage response;
                
                //get all items
                response = await client.GetAsync("api/stocks/");
                if (response.IsSuccessStatusCode)
                { 
                    json = await response.Content.ReadAsStringAsync();
                    IEnumerable<Stock> items = JsonConvert.DeserializeObject<IEnumerable<Stock>>(json);
                    foreach (Stock stock in items)
                    {
                        Console.WriteLine(stock.Name);
                    }
                }
                else
                    Console.WriteLine("Internal Server error");
                
                // get the specified item
                string symbol = "BMO";
                Stock item;
                response = await client.GetAsync("api/stock/" + symbol);
                if (response.IsSuccessStatusCode)
                {
                    item = await response.Content.ReadAsAsync<Stock>();
                    //json = await response.Content.ReadAsStringAsync();
                    //Console.WriteLine(json);
                    //item = JsonConvert.DeserializeObject<TodoItem>(json);
                    Console.WriteLine("\nReturn individual stock details:\n " + item.Name + " - " + item.Symbol);
                }
                else Console.WriteLine("Internal Server error");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }*/
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }

}
