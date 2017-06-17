using System;
using System.IO;
using Nancy;
using Nancy.Hosting.Self;
using Newtonsoft.Json;
using SlackPOC.Core;

namespace SlackPOC
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new HostConfiguration() { UrlReservations = new UrlReservations() { CreateAutomatically = true } };
            using (var host = new NancyHost(config, new Uri("http://localhost:9012")))
            {
                host.Start();
                
                var oAuthToken = GetTokensAndShit.GetOAuthToken();
                var authedClient = SlackClientStuff.GetSlackClient(oAuthToken);

                authedClient?.PostMessage(
                    result => Console.WriteLine("Response was: " + JsonConvert.SerializeObject(result, Formatting.Indented)),
                    Constants.CHANNEL_NAME,
                    "Test!");

                Console.WriteLine("Please press [Enter] to exit the application.");
                Console.ReadLine();
            }
        }
    }
}