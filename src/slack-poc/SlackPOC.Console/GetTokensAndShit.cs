using System;
using Microsoft.AspNetCore.WebUtilities;
using SlackAPI;
using SlackPOC.Core;

namespace SlackPOC
{
    public class GetTokensAndShit
    {
        public static OAuthResponse CompleteOAuth()
        {
            var state = Guid.NewGuid().ToString();
            
            var uri = SlackClient.GetAuthorizeUri(Constants.CLIENT_ID, 
                SlackScope.Identify | SlackScope.Read | SlackScope.Post, 
                Constants.CALLBACK_URI, 
                state, 
                Constants.TEAM_NAME);

            Console.WriteLine("Launching: " + uri);

            OpenBrowserHack.OpenBrowser(uri.ToString());

            // Come back here and make the HTTP response land in our console app - it's .Net Core after all :)
            Console.WriteLine("Please paste in the URL of the authentication result...");
            var asString = Console.ReadLine();
            var index = asString.IndexOf('?');
            if (index != -1)
                asString = asString.Substring(index + 1);

            var qs = QueryHelpers.ParseQuery(asString);
            var code = qs["code"];
            var newState = qs["state"];

            // validate the state. this isn't required, but let's makes sure the request and response line up...
            if (state != newState)
                throw new InvalidOperationException("State mismatch. You're trying to trick me!");

            return new OAuthResponse(code, newState);
        }
    }
}
