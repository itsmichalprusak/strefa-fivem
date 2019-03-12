using System;
using System.Collections.Generic;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace Admin.Client
{
    public class cmdCar : BaseScript
    {
        public cmdCar()
        {
            EventHandlers["onClientResourceStart"] += new Action<string>(OnClientResourceStart);
        }

        private void OnClientResourceStart(string resourceName)
        {
            if (GetCurrentResourceName() != resourceName) return;

            RegisterCommand("car", new Action<int, List<object>, string>(async (source, args, raw) =>
            {
                // sprawdzenie czy argument został przekazany
                var model = "adder";
                if (args.Count > 0)
                {
                    model = args[0].ToString();
                }

                // Sprawdza czy model istnieje
                // Oczywiscie za pomocą API CitizeFX
                var hash = (uint) GetHashKey(model);
                if (!IsModelInCdimage(hash) || !IsModelAVehicle(hash))
                {
                    TriggerEvent("chat:addMessage", new 
                    {
                        color = new[] { 255, 0, 0 },
                        args = new[] { "AdmCmd:", $"Błąd! Podany model: {model} nie istnieje!" }
                    });
                    return;
                }

                // Tworzenie pojazdu
                var vehicle = await World.CreateVehicle(model, Game.PlayerPed.Position, Game.PlayerPed.Heading);
    
                // Daje gracza do pojazdu
                Game.PlayerPed.SetIntoVehicle(vehicle, VehicleSeat.Driver);

                // Wiadomość zwrotna do gracza
                TriggerEvent("chat:addMessage", new 
                {
                    color = new[] {255, 0, 0},
                    args = new[] {"AdmCmd:", $"Stworzyłeś nowy pojazd {model}!"}
                });
            }), false);
        }
    }
}