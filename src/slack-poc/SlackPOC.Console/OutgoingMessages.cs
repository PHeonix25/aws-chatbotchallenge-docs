using System;
using SlackAPI;
using SlackPOC.Core;

namespace SlackPOC
{
    public class OutgoingMessages
    {
        public static void PostMessage(OAuthResponse oAuthResponse, string message)
        {
            try
            {
                // then get the token...
                Console.WriteLine("Requesting access token...");
                SlackClient.GetAccessToken((response) =>
                {
                    var accessToken = response.access_token;
                    if (string.IsNullOrWhiteSpace(accessToken))
                        Console.WriteLine("AccessToken was not provided. Error reason: " + response.error);
                    else
                    {
                        Console.WriteLine("Got access token '{0}'...", accessToken);

                        //post...
                        var client = new SlackClient(accessToken);
                        client.PostMessage((r) => Console.WriteLine(r), Constants.CHANNEL_NAME, message);
                    }

                    // finished...
                    Console.WriteLine("Done.");

                }, Constants.CLIENT_ID, Constants.CLIENT_SECRET, Constants.CALLBACK_URI, oAuthResponse.OAuthToken);

                Console.WriteLine("Awaiting results of the AccessToken callback");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                Console.ReadLine();
            }
        }
    }
}
