using System;
using SlackAPI;
using SlackPOC.Core;

namespace SlackPOC
{
    public static class SlackClientStuff
    {
        public static SlackClient GetSlackClient(string oAuthToken)
        {
            SlackClient client = null;
            var complete = false;
            try
            {
                Action<AccessTokenResponse> callback = response =>
                {
                    Console.WriteLine(
                        string.IsNullOrWhiteSpace(response.access_token) 
                            ? "AccessToken was not provided. Error reason: " + response.error 
                            : "Got access token '{0}'...", response.access_token);
                        
                    client = new SlackClient(response.access_token);
                    Console.WriteLine("Done.");
                    complete = true;
                };

                Console.WriteLine("Requesting access token...");
                SlackClient.GetAccessToken(callback, Constants.CLIENT_ID, Constants.CLIENT_SECRET, Constants.CALLBACK_URI, oAuthToken);
                Console.WriteLine("Awaiting results of the AccessToken callback");
                while (!complete) { };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return client;
        }

        public static SlackClient IWantAClient()
        {
            return GetSlackClient(GetTokensAndShit.GetOAuthToken(false));
        }
    }
}
