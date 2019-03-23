using System;
using System.Collections.Generic;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace Admin.Client
{
    public class CmdDelCar : BaseScript
    {
        public CmdDelCar()
        {
            EventHandlers["onClientResourceStart"] += new Action<string>(OnClientResourceStart);
        }

        private void OnClientResourceStart(string resourceName)
        {
            if (GetCurrentResourceName() != resourceName) return;

            RegisterCommand("delcar", new Action<int, List<object>, string>(async (source, args, raw) =>
            {
                // Sprawdzanie czy gracz jest w pojeździe
                var vehicle = Game.PlayerPed.CurrentVehicle;
                if (vehicle == null)
                {
                    TriggerEvent("chat:addMessage", new
                    {
                        color = new[] {255, 0, 0},
                        args = new[] {"AdmCmd", "Nie jesteś w pojeździe!"}
                    });
                    return;
                }

                // Usuwanie pojazdu
                vehicle.Delete();
                // Wiadomość zwrotna do gracza
                TriggerEvent("chat:addMessage", new
                {
                    color = new[] {255, 0, 0},
                    args = new[] {"AdmCmd", "Pojazd został usunięty!"}
                });
                await Delay(1000);
            }), false);
        }
    }
}