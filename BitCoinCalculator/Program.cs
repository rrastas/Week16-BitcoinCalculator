using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace BitCoinCalculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the number of your bitocoins:");
            float userCoins = float.Parse(Console.ReadLine());
            BitCoinRate currentBicoin = GetRates();
            
            float result = userCoins * currentBicoin.bpi.EUR.rate_float;
            Console.WriteLine($"Your bitcoins are {result} {currentBicoin.bpi.EUR.code} worth.");
            Console.WriteLine(currentBicoin.disclaimer);

        }

        public static BitCoinRate GetRates()
        {
            string url = "https://api.coindesk.com/v1/bpi/currentprice.json";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            var webResponse = request.GetResponse();
            var webStream = webResponse.GetResponseStream();

            BitCoinRate bitcoin;

            using (var responseReader = new StreamReader(webStream))
            {
                var response = responseReader.ReadToEnd();
                bitcoin = JsonConvert.DeserializeObject<BitCoinRate>(response);
            }


            return bitcoin;

        }
    }
}
