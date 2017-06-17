using System;
using System.Collections.Generic;
using System.Text;

namespace SlackPOC.Core.Models
{
    public class HookMessage
    {
        public string Token { get; set; }
        public string TeamId { get; set; }
        public string ChannelId { get; set; }
        public string ChannelName { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Text { get; set; }
        public string TriggerWord { get; set; }
    }
}
