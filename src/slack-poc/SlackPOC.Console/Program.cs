namespace SlackPOC
{
    class Program
    {
        static void Main(string[] args)
        {
            var auth = GetTokensAndShit.CompleteOAuth();
            OutgoingMessages.PostMessage(auth, "Test!");
        }
    }
}