using System;
using System.Collections.Generic;
using System.Linq;
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
            API.RegisterCommand("kick", new Action<int, List<object>, string>((source, args, raw) =>
            {
                // Sprawdzenie czy gracz jest na serwerze
                int id = -1;

                if (Int32.TryParse(args[0].ToString(), out id))
                {
                    var online = false;

                    for (var i = 0; i < Players.Count(); i++)
                    {
                        if (Players.ToList()[i].ServerId == id)
                            online = true;
                    }
        
                    if (!online)
                    {
                        Debug.WriteLine($"Player: [ID:{id}] is offline(?)");
                        TriggerEvent("chat:addMessage", new 
                        {
                            color = new[] { 255, 0, 0 },
                            args = new[] { "AdmCmd", $"Błąd! Gracz o  [ID:{id}] nie znajduje się na serwerze!" }
                        });
                    }
                    else
                    {
                        Debug.WriteLine($"Player: [ID:{id}] is online(?)");
                        TriggerServerEvent("srp_admin:kick");
                        TriggerEvent("chatMessage", $"Gracz o [ID:{id}] wyleciał z serwera!");
                    }
                }
            }), false);
        }
    }
}