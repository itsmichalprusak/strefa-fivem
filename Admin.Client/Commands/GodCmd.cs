using System;
using System.Collections.Generic;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace Admin.Client.Commands
{
    public class GodCmd : BaseScript
    {
        private bool _pGodMode = false;
        public GodCmd()
        {
            API.RegisterCommand("god", new Action<int, List<object>, string>(OnAdminGodMode), false);
        }
        
        private async void OnAdminGodMode(int p1, List<object> args, string p2)
        {
            // Jeśli Admin jest na Aduty wykonuje się komenda
            // TO:DO Dodać dodatkowe uprawnienia
            if (AdutyCmd.pAduty)
            {
                 _pGodMode = !this._pGodMode;
                 
                 // Pobiera nasze ID z serwera
                 int playerId = API.PlayerPedId();
                 
                 // Jeśli pGodMode jest true to uruchamia się GodMode
                 if (_pGodMode)
                 {
                    API.SetEntityInvincible(playerId, true);
                    API.SetPlayerInvincible(playerId, true);
                    API.SetPedCanRagdoll(playerId, false);
                    API.ClearPedBloodDamage(playerId);
                    API.ResetPedVisibleDamage(playerId);
                    API.ClearPedLastWeaponDamage(playerId);
                    API.SetEntityProofs(playerId, true, true, true, true, true, true, true, true);
                    API.SetEntityOnlyDamagedByPlayer(playerId, false);
                    API.SetEntityCanBeDamaged(playerId, false);
                    TriggerEvent("chat:addMessage", new
                    {
                        args = new[] {$"^1[ADMIN]: ^0GodMode: ^2 Włączony"}
                    });
                    await Delay(100);
                 }
                 
                 // Jeśli pGodMode jest false to GodMode się wyłącza
                 else if (_pGodMode == false)
                 {
                     API.SetEntityInvincible(playerId, false);
                     API.SetPlayerInvincible(playerId, false);
                     API.SetPedCanRagdoll(playerId, true);
                     API.ClearPedLastWeaponDamage(playerId);
                     API.SetEntityProofs(playerId, false, false, false, false, false, false, false, false);
                     API.SetEntityOnlyDamagedByPlayer(playerId, true);
                     API.SetEntityCanBeDamaged(playerId, true);
                     TriggerEvent("chat:addMessage", new
                     {
                         args = new[] {$"^1[ADMIN]: ^0GodMode: ^1 Wyłączony"}
                     });
                     await Delay(100);
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