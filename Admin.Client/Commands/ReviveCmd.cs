using System;
using System.Collections.Generic;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace Admin.Client.Commands
{
    public class ReviveCmd : BaseScript
    {
        public ReviveCmd()
        {
            API.RegisterCommand("revive", new Action<int, List<object>, string>(OnAdminPlayerRevive), false);
        }

        private void OnAdminPlayerRevive(int p1, List<object> args, string p2)
        {
            // Jeśli Admin jest na Aduty wykonuje się komenda
            // TO:DO Dodać dodatkowe uprawnienia
            if (AdutyCmd.pAduty)
            {
                var position = Game.Player.Character.Position;
                API.NetworkResurrectLocalPlayer(position.X, position.Y, position.Z, Game.Player.Character.Heading, false, false);   
            }
            
            // Jesli Admin nie jest na aduty nie dostaje żadnej wiadomości zwrotnej.
            else if (AdutyCmd.pAduty == false)
            {
                return;
            }
        }
    }
}