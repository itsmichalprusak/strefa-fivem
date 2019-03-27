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

        private static void OnClientResourceStart(string resourceName)
        {
            API.RegisterCommand("kick", new Action<int, List<object>, string>((source, args, raw) =>
            {
                // Sprawdzenie czy gracz jest na serwerze
                var reason = string.Join(" ", args.GetRange(1, args.Count - 1));
                if (!int.TryParse(args[0].ToString(), out var id)) return;
                var playerId = API.GetPlayerFromServerId(id);
                var online = API.NetworkIsPlayerActive(playerId);

                // Jesli gracz jest offline -> Wiadomosc zwrotna ze jest offline.
                if (!online)
                {
                    TriggerEvent("chat:addMessage", new
                    {
                        args = new[] {$"^1AdmCmd: ^0Gracz o [ID:^1{id}^0] jest offline!"}
                    });
                }
                // Jesli grasz jest online -> Wysylanie globalnej wiadomosci do serwera + triggerowanie ewentu po stronie serwera.
                else
                {
                    TriggerEvent("chatMessage", $"^1AdmCmd: ^0Gracz o [ID:^1{id}^0] wyleciał z serwera!");
                    TriggerServerEvent("srp_admin:kick", id, string.IsNullOrEmpty(reason) ? "Nie podano powodu." : reason);
                }
            }), false);
        }
    }
}