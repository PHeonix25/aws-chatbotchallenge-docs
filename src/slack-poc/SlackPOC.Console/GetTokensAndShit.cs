using System;
using SlackAPI;
using SlackPOC.Core;

namespace SlackPOC
{
    public class GetTokensAndShit
    {
        private const string OAUTH_FILE = ".oauth";
        private static string STATE = Guid.NewGuid().ToString();
        private static bool RESULT_PENDING = false;
        private static string RESULT = String.Empty;

        public static string GetOAuthToken(bool forceReplace = true)
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
            var uri = SlackClient.GetAuthorizeUri(Constants.CLIENT_ID,
                SlackScope.Identify | SlackScope.Read | SlackScope.Post,
                Constants.CALLBACK_URI,
                STATE,
                Constants.TEAM_NAME);

            Console.WriteLine("Launching: " + uri);

            OpenBrowserHack.OpenBrowser(uri.ToString());
            RESULT_PENDING = true;
            while (RESULT_PENDING) { }
            return RESULT;
        }

        public static void HereComesAResult(string code, string state)
        {
            // validate the state. this isn't required, but let's makes sure the request and response line up...
            if (STATE != state)
                throw new InvalidOperationException("State mismatch. You're trying to trick me!");

            RESULT = code;
            RESULT_PENDING = false;
        }

        private static string LoadTokenFromFile() =>
            (System.IO.File.Exists(OAUTH_FILE))
                ? System.IO.File.ReadAllText(OAUTH_FILE)
                : String.Empty;

        private static void SaveTokenToFile(string token) => System.IO.File.WriteAllText(OAUTH_FILE, token);
    }
}
