using System;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace Admin.Server
{
    internal class Kick : BaseScript
    {
        public Kick()
        {
            EventHandlers["srp_admin:kick"] += new Action<Player, int, string>(OnPlayerKick);
        }

        private static void OnPlayerKick([FromSource] Player source, int id, string reason)
        {
            // Funckja odpowiedzialna za kicki
            API.DropPlayer(id.ToString(), $"Kick: {reason}");
        }
    }
}