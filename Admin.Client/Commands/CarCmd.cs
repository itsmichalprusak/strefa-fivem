using System;
using System.Collections.Generic;
using CitizenFX.Core;
using CitizenFX.Core.Native;


namespace Admin.Client.Commands
{
    public class CarCmd : BaseScript
    {
        public CarCmd()
        {
            API.RegisterCommand("car", new Action<int, List<object>, string>(OnAdminSpawnCar), false);
        }
        
        private async void OnAdminSpawnCar(int p1, List<object> args, string p2)
        {
            // Jeśli Admin jest na Aduty wykonuje się komenda
            // TO:DO Dodać dodatkowe uprawnienia
            if (AdutyCmd.pAduty)
            {
                // sprawdzenie czy argument został przekazany
                var model = "adder";
                if (args.Count > 0) model = args[0].ToString();

                // Sprawdza czy model istnieje
                // Oczywiscie za pomocą API CitizeFX
                var hash = (uint) API.GetHashKey(model);
                if (!API.IsModelInCdimage(hash) || !API.IsModelAVehicle(hash))
                {
                    TriggerEvent("chat:addMessage", new
                    {
                        args = new[] {$"^1[ADMIN]: ^0Pojazd o modelu: {model} nie istnieje"}
                    });
                    return;
                }

                // Tworzenie pojazdu
                var vehicle = await World.CreateVehicle(model, Game.PlayerPed.Position, Game.PlayerPed.Heading);

                // Daje gracza do pojazdu
                Game.PlayerPed.SetIntoVehicle(vehicle, VehicleSeat.Driver);
                // Pobieranie informacji czy gracz jest w pojeździe.
                //var pedinveh = API.GetVehiclePedIsIn(API.PlayerPedId(), false);
                // Ustawia customową tablice rejestracyjną dla respionego pojazdu.
                //API.SetVehicleNumberPlateText(pedinveh, "ADMIN");
                //API.SetVehicleNumberPlateTextIndex(pedinveh, 1);

                // Wiadomość zwrotna do gracza
                TriggerEvent("chat:addMessage", new
                {
                    args = new[] {$"^1[ADMIN]: ^0Stworzyłeś pojazd o modelu: {model}"}
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