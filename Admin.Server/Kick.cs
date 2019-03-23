using System;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace Admin.Server
{
    class Kick : BaseScript
    {
        public Kick()
        {
            EventHandlers["srp_admin:kickReson"] += new Action<Player,int,string>(OnPlayerKickReson);
            EventHandlers["srp_admin:kick"] += new Action<Player, int>(OnPlayerKick);
        }

        // Defaultowy powod kicka
        private void OnPlayerKick([FromSource] Player source, int id)
        {
            OnPlayerKickReson(source, id, "You've been kicked");
        }

        // Kick sam w sobie
        private void OnPlayerKickReson([FromSource] Player source, int id, string reason)
        {
            // Debug w konsoli serwera
            Debug.WriteLine($"[DeBug] SerwerEwent Kick dla id: '{id}' => '{reason}'");
            // Funckja odpowiedzialna za kicki
            API.DropPlayer(id.ToString(), $"Kick: {reason}");
        }
    }
}