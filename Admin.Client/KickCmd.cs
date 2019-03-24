using System;
using System.Collections.Generic;
using CitizenFX.Core;
using CitizenFX.Core.Native;

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
            API.RegisterCommand("kick", new Action<int, List<object>, string>((source, args, raw) =>
            {
                // Sprawdzenie czy gracz jest na serwerze
                int id = -1;
                if (int.TryParse(args[0].ToString(), out id))
                {
                    int playerId = API.GetPlayerFromServerId(id);
                    bool online = API.NetworkIsPlayerActive(playerId);

                    // Jesli gracz jest offline -> Wiadomosc zwrotna ze jest offline.
                    if (!online)
                    {
                        TriggerEvent("chat:addMessage", $"^1AdmCmd: ^0Gracz o [ID:^1{id}^0] jest offline!");
                    }
                    // Jesli grasz jest online -> Wysylanie globalnej wiadomosci do serwera + triggerowanie ewentu po stronie serwera.
                    else
                    {
                        TriggerEvent("chatMessage", $"^1AdmCmd: ^0Gracz o [ID:^1{id}^0] wyleciał z serwera!");
                        TriggerServerEvent("srp_admin:kick", id);
                    }
                }
            }), false);
        }
    }
}