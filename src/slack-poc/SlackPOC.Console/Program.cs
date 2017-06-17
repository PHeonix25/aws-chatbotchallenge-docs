using System;
using Newtonsoft.Json;
using SlackPOC.Core;

namespace SlackPOC
{
    class Program
    {
        static void Main(string[] args)
        {
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