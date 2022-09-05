using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event = Exiled.Events.Handlers;

namespace scp096timer
{
    public class Plugin : Plugin<Config>
    {
        public static Plugin Instance;
        public override string Author { get; } = "sky";

        public override string Name { get; } = "Scp096Config";

        public override string Prefix { get; } = "Scp096";

        public override Version Version { get; } = new Version(1, 0, 0);

        public override Version RequiredExiledVersion { get; } = new Version(5, 3, 0);

        public static Plugin Singleton;
        public EventHandlers EventHandlers { get; private set; }

        public override void OnEnabled()
        {
            Singleton = this;
            Instance = this;
            EventHandlers = new EventHandlers(this);
            Event.Scp096.Enraging += EventHandlers.enraging;
            Event.Scp096.AddingTarget += EventHandlers.AddTarget;
            Event.Scp096.CalmingDown += EventHandlers.calmingDown;
            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            Event.Scp096.Enraging -= EventHandlers.enraging;
            Event.Scp096.AddingTarget -= EventHandlers.AddTarget;
            Event.Scp096.CalmingDown -= EventHandlers.calmingDown;
            Singleton = null;
            EventHandlers = null;
            base.OnDisabled();
        }
    }
}
