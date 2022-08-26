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
    }
}
