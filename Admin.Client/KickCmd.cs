using System;
using System.Collections.Generic;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using static CitizenFX.Core.Native.API;

namespace Admin.Client
{
    public class KickCmd : BaseScript
    {
        public KickCmd()
        {
            EventHandlers["onClientResourceStart"] += new Action<string>(OnClientResourceStart);
        }

        private void OnClientResourceStart(string resourceName)
        {
            if (GetCurrentResourceName() != resourceName) return;
            
            RegisterCommand("kick", new Action<int, List<object>, string>(async (source, args, raw) =>
                {
                    // Sprawdzenie czy gracz jest na serwerze
                    var playerId = GetPlayerServerId(PlayerId());
                    var online = NetworkIsPlayerActive(PlayerId());
                    Debug.Write($"{online}");
                    if (!online)
                    {
                        // Wiadomosc zwrota ze gracz nie jest na serwerze
                        TriggerEvent("chat:addMessage", new 
                        {
                            color = new[] { 255, 0, 0 },
                            args = new[] { "AdmCmd", $"Błąd! Gracz o  [ID:{playerId}] nie znajduje się na serwerze!" }
                        });
                        return;
                    }
                    
                    TriggerServerEvent("srp_admin:kick");
                    await Delay(1000);
                }), false);
        }
    }
}