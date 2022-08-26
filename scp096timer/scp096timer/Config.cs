using Exiled.API.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scp096timer
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = true;

        [Description("Syntaxe : broadcast, Hint")]
        public string TypeOfMessage { get; set; } = "Hint";
        public bool EnableTarget { get; set; } = true;
    }
}
