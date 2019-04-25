using System;
using System.Collections.Generic;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace Admin.Client.Commands
{
    public class NoclipCmd : BaseScript
    {
        private bool _pNoclip = false;
        public NoclipCmd()
        {
            API.RegisterCommand("noclip", new Action<int, List<object>, string>(OnAdminNoclip), false);
        }
        
        private async void OnAdminNoclip(int p1, List<object> args, string p2)
        {
            // Jeśli Admin jest na Aduty wykonuje się komenda
            // TO:DO Dodać dodatkowe uprawnienia
            if (AdutyCmd.pAduty)
            {
                _pNoclip = !this._pNoclip;
                 
                 // Pobiera nasze ID z serwera
                 int playerId = API.PlayerPedId();
                 
                 // Pobieranie kodynatow X, Y, Z gracza
                 Vector3 noclipPos = API.GetEntityCoords(playerId, true);
                 
                 // Jeśli pGodMode jest true to uruchamia się GodMode
                 if (_pNoclip)
                 {
                    API.SetEntityCoordsNoOffset(playerId, noclipPos.X, noclipPos.Y, noclipPos.Z, false, false, false);

                    if (API.IsControlPressed(1, 34))
                    {
                        
                    }
                 }
                 
                 // Jeśli pGodMode jest false to GodMode się wyłącza
                 else if (_pNoclip == false)
                 {

                 }
            }
            
            // Jesli Admin nie jest na aduty nie dostaje żadnej wiadomości zwrotnej.
            else if (AdutyCmd.pAduty == false)
            {
                return;
            }
        }
    }
}