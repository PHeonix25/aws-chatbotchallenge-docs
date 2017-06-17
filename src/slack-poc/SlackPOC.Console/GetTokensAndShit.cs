using System;
using Microsoft.AspNetCore.WebUtilities;
using SlackAPI;
using SlackPOC.Core;

namespace SlackPOC
{
    public class GetTokensAndShit
    {
        private const string OAUTH_FILE = ".oauth";

        public static string GetOAuthToken(bool forceReplace = false)
        {
            var existingToken = LoadTokenFromFile();
            if (!String.IsNullOrEmpty(existingToken) && !forceReplace)
                return existingToken;

            var oauth = CompleteOAuth();
            SaveTokenToFile(oauth);
            return oauth;
        }

        private static string CompleteOAuth()
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

            return code;
        }

        private static string LoadTokenFromFile() =>
            (System.IO.File.Exists(OAUTH_FILE))
                ? System.IO.File.ReadAllText(OAUTH_FILE)
                : String.Empty;

        private static void SaveTokenToFile(string token) => System.IO.File.WriteAllText(OAUTH_FILE, token);
    }
}
