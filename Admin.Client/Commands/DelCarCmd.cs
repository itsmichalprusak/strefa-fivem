using System;
using System.Collections.Generic;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace Admin.Client.Commands
{
    public class DelCarCmd : BaseScript
    {
        public DelCarCmd()
        {
            API.RegisterCommand("delcar", new Action<int, List<object>, string>(OnAdminDelCar), false);
        }

        //private void OnClientResourceStart(string resourceName)
        private async void OnAdminDelCar(int p1, List<object> args, string p2)
        {
            // Jeśli Admin jest na Aduty wykonuje się komenda
            // TO:DO Dodać dodatkowe uprawnienia
            if (AdutyCmd.pAduty)
            {
                // Sprawdzanie czy gracz jest w pojeździe
                var vehicle = Game.PlayerPed.CurrentVehicle;

                if (vehicle == null)
                {
                    TriggerEvent("chat:addMessage", new
                    {
                        args = new[] {"^1[ADMIN]: ^0Nie znajdujesz się pojeździe!"}
                    });
                    return;
                }

                // Usuwanie pojazdu
                vehicle.Delete();
                // Wiadomość zwrotna do gracza
                TriggerEvent("chat:addMessage", new
                {
                    args = new[] {"^1[ADMIN]: ^0Pojazd został usunięty"}
                });
                await Delay(1000);
            }
            
            // Jesli Admin nie jest na aduty nie dostaje żadnej wiadomości zwrotnej.
            else if (AdutyCmd.pAduty == false)
            {
                return;
            }
        }
    }
}