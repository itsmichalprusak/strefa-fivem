using System;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace Admin.Server
{
    internal class Kick : BaseScript
    {
        public Kick()
        {
            EventHandlers["srp_admin:kickReson"] += new Action<Player, int, string>(OnPlayerKickReson);
            EventHandlers["srp_admin:kick"] += new Action<Player, int>(OnPlayerKick);
        }

        // Defaultowy powod kicka
        private void OnPlayerKick([FromSource] Player source, int id)
        {
            OnPlayerKickReson(source, id, "You've been kicked");
        }

        private void OnPlayerKickReson([FromSource] Player source, int id, string reason)
        {
            // Funckja odpowiedzialna za kicki
            API.DropPlayer(id.ToString(), $"Kick: {reason}");
        }
    }
}