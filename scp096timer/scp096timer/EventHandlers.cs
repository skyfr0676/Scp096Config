using Exiled.API.Features;
using Exiled.API.Features.Roles;
using Exiled.Events.EventArgs;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BroadcastTyped;

namespace scp096timer
{
    public class EventHandlers : Plugin<Config>
    {
        private readonly Plugin _plugin;
        internal EventHandlers(Plugin plugin) => this._plugin = plugin;

        public List<Player> players = new List<Player>();
        public void enraging(EnragingEventArgs ev)
        {
            Timing.RunCoroutine(AddCoroutine(ev),"enrage");
        }
        public void AddTarget(AddingTargetEventArgs ev)
        {
            players.Add(ev.Target);
        }
        public IEnumerator<float> AddCoroutine(EnragingEventArgs ev)
        {
            for (; ; )
            {
                yield return Timing.WaitForSeconds(1f);
                string timeleft = $"{ev.Scp096.EnrageTimeLeft}";
                string[] sec = timeleft.Split('.');
                string status = $"{Plugin.Singleton.Config.MessageEnraged} {sec[0]}</color>\n";
                string target = $"{Plugin.Singleton.Config.MessageTarget}\n";
                if (Config.EnableTarget)
                {
                    foreach (Player player in players)
                    {
                        target += $"<color=red>{player.DisplayNickname} : {player.Zone}</color>\n";
                    }
                }
                switch (Plugin.Singleton.Config.TypeOfMessage)
                {
                    default:
                        if (Plugin.Singleton.Config.EnableTarget)
                            ev.Player.ShowHint(status + target, 1);
                        else
                            ev.Player.ShowHint(status, 1);
                        break;
                    case BroadcastType.Broadcast:
                        if (Plugin.Singleton.Config.EnableTarget)
                        {
                            ev.Player.Broadcast(1, status);
                            ev.Player.ShowHint(target, 1);
                        }
                        else
                            ev.Player.Broadcast(1, status);
                        break;
                    case BroadcastType.Hint:
                        if (Plugin.Singleton.Config.EnableTarget)
                            ev.Player.ShowHint(status + target, 1);
                        else
                            ev.Player.ShowHint(status, 1);
                        break;
                }

            }
        }
        public void calmingDown(CalmingDownEventArgs ev)
        {
            Timing.KillCoroutines("enrage");
        }
    }
}
