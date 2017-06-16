namespace SlackPOC.Core
{
    public class OAuthResponse
    {
        public OAuthResponse(string oAuthToken, string requestIdentifier)
        {
            OAuthToken = oAuthToken;
            RequestIdentifier = requestIdentifier;
        }

        public string OAuthToken { get; private set; }
        public string RequestIdentifier { get; private set; }
    }
}
