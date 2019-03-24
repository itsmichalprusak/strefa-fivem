using System;
using System.Collections.Generic;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace Admin.Client
{
    public class AdutyCmd : BaseScript
    {
        public AdutyCmd()
        {
            EventHandlers["onClientResourceStart"] += new Action<string>(OnClientResourceStart);
        }

        private void OnClientResourceStart(string resourceName)
        {
            API.RegisterCommand("aduty", new Action<int, List<object>, string>((source, args, raw) =>
            {
                int pAduty = 1;
                string pAdmin = "Admin";
                if (pAduty > 0)
                {
                    TriggerEvent("chat:addMessage", $"^1[Admin]: ^0 Wchodzisz na służbę {pAdmin}");
                }
                else if()
                {
                    TriggerEvent("chat:addMessage", $"^1[Admin]: ^0 Schodzisz z służby {pAdmin}");
                }

            }), false);
        }
    }
}