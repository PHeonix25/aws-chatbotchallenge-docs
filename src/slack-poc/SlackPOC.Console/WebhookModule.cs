using Nancy;
using Nancy.ModelBinding;

namespace SlackPOC
{
    public class WebhookModule : NancyModule
    {
        public WebhookModule()
        {
            Get["/hello"] = parameters => "HI!";
            Get["/"] = _ =>
            {
                var code = Request.Query["code"];
                var state = Request.Query["state"];
                GetTokensAndShit.HereComesAResult(code, state);
                return "Processed";
            };

            Post["/"] = _ =>
            {
                var model = this.Bind<Core.Models.HookMessage>();
                var message = string.Empty;
                if (model.Text.ToLower().StartsWith("testbot: hello"))
                    message = string.Format("@{0} Hello", model.UserName);
                if (!string.IsNullOrWhiteSpace(message))
                    SlackClientStuff.IWantAClient().PostMessage(null, Core.Constants.CHANNEL_NAME, message);
                return null;
            };
        }
    }
}
