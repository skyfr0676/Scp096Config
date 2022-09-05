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
    public class EventHandlers
    {
        private readonly Plugin _plugin;
        internal EventHandlers(Plugin plugin) => this._plugin = plugin;

        public void enraging(EnragingEventArgs ev)
        {
            Timing.RunCoroutine(AddCoroutine(ev),$"{ev.Player.UserId}'s enrage");
        }

        public void AddTarget(AddingTargetEventArgs ev)
        {
            switch (Plugin.Singleton.Config.TypeOfTargetTargetMessage)
            {
                default:
                    ev.Target.ShowHint(Plugin.Singleton.Config.ShowTargetViewScp096Face, 10);
                    break;
                case BroadcastType.Hint:
                    ev.Target.ShowHint(Plugin.Singleton.Config.ShowTargetViewScp096Face, 10);
                    break;
                case BroadcastType.Broadcast:
                    ev.Target.Broadcast(10, Plugin.Singleton.Config.ShowTargetViewScp096Face);
                    break;
            }
            switch (Plugin.Singleton.Config.TypeOfTargetScp096Message)
            {
                default:
                    ev.Target.ShowHint($"{ev.Target.DisplayNickname} {Plugin.Singleton.Config.ShowScp096Target} <color={ev.Target.Role.Color.ToHex()}>{ev.Target.Role.Type}</color>.", 10);
                    break;
                case BroadcastType.Hint:
                    ev.Target.ShowHint($"{ev.Target.DisplayNickname} {Plugin.Singleton.Config.ShowScp096Target} <color={ev.Target.Role.Color.ToHex()}>{ev.Target.Role.Type}</color>.", 10);
                    break;
                case BroadcastType.Broadcast:
                    ev.Target.Broadcast(10, $"{ev.Target.DisplayNickname} {Plugin.Singleton.Config.ShowScp096Target} <color={ev.Target.Role.Color.ToHex()}>{ev.Target.Role.Type}</color>.");
                    break;
            }
        }

        public IEnumerator<float> AddCoroutine(EnragingEventArgs ev)
        {
            for (; ; )
            {
                yield return Timing.WaitForSeconds(1f);
                string timeleft = $"{ev.Scp096.EnrageTimeLeft}";
                if (ev.Player.Role is Scp096Role scp096)
                {
                    foreach (Player test in scp096.Targets)
                    {
                        foreach (Player player in scp096.Targets)
                        {
                            string[] sec = timeleft.Split('.');
                            string status = $"{Plugin.Singleton.Config.MessageEnraged} {sec[0]}</color>\n";
                            string target = $"{Plugin.Singleton.Config.MessageTarget}\n";
                            if (Plugin.Singleton.Config.EnableTarget)
                            {
                                target += $"<color=red>{player.DisplayNickname} : {player.Zone}</color>\n";
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
                }
                else
                {
                    Timing.KillCoroutines($"{ev.Player.UserId}'s enrage");
                }
            }
        }
        public void calmingDown(CalmingDownEventArgs ev)
        {
            Timing.KillCoroutines($"{ev.Player.UserId}'s enrage");
        }
    }
}
