using System;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace Admin.Client
{
    public class PvP : BaseScript
    {
        public PvP()
        {
            EventHandlers["onClientResourceStart"] += new Action<string>(OnClientResourceStart);
        } 
        private void OnClientResourceStart(string resourceName)
        {
            if (GetCurrentResourceName() != resourceName) return;
            // Uruchamia Friendly Fire
            NetworkSetFriendlyFireOption(true);
            // Uruchamia Zadawanie obrażeń między graczami
            SetCanAttackFriendly(PlayerPedId(), true, true);
        }
    }
}