﻿using Exiled.API.Features;
using Exiled.API.Features.Roles;
using Exiled.Events.EventArgs;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

                string status = $"you are enraged for {sec[0]}\n";
                string target = "<color=red>youre target has :\n</color>";
                if (Config.EnableTarget)
                {
                    foreach (Player player in players)
                    {
                        target += $"<color=red>{player.DisplayNickname} : {player.Zone}</color>\n";
                    }
                }
                if (Config.TypeOfMessage == "Hint")
                {
                    status += target;
                    ev.Player.ShowHint(status, 1);
                }
                if (Config.TypeOfMessage == "broadcast")
                {
                    ev.Player.Broadcast(1, status);
                    if (Config.EnableTarget)
                        ev.Player.ShowHint(target, 1);
                }

            }
        }
        public void calmingDown(CalmingDownEventArgs ev)
        {
            Timing.KillCoroutines("enrage");
        }
    }
}