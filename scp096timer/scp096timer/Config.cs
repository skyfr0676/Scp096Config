using BroadcastTyped;
using Exiled.API.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;

namespace scp096timer
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = true;

        [Description("Syntaxe : Broadcast, Hint. Default : Hint.")]
        public BroadcastType TypeOfMessage { get; set; } = BroadcastType.Hint;
        public bool EnableTarget { get; set; } = true;

        public string MessageEnraged { get; set; } = "<color=red>you are enraged for";

        public string MessageTarget { get; set; } = "<color=red>youre target was : </color>";

        public string ShowScp096Target { get; set; } = "has saw yout face . it's ";

        public string ShowTargetViewScp096Face { get; set; } = "you have saw Scp096 Face !";

        public BroadcastType TypeOfTargetScp096Message { get; set; } = BroadcastType.Hint;

        public BroadcastType TypeOfTargetTargetMessage { get; set; } = BroadcastType.Broadcast;
    }
}
