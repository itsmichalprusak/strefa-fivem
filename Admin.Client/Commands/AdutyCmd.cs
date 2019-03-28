using System;
using System.Collections.Generic;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace Admin.Client.Commands
{
    public class AdutyCmd : BaseScript
    {
        public static bool pAduty = false;
        public AdutyCmd()
        {
            API.RegisterCommand("aduty", new Action<int, List<object>, string>(OnAdminAduty), false);
        }
        
        public void OnAdminAduty(int p1, List<object> args, string p2)
        {
            pAduty = !AdutyCmd.pAduty;
            
            if (pAduty)
            {
                TriggerEvent("chat:addMessage", new
                {
                    args = new[] {$"^1[ADMIN]: ^0Wchodzisz na służbę Administratora."}
                });
            }
            else if (pAduty == false)
            {
                TriggerEvent("chat:addMessage", new
                {
                    args = new[] {$"^1[ADMIN]: ^0Schodzisz z służby Administratora."}
                });  
            }
        }
    }
}