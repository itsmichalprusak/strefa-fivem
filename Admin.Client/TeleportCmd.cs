using System;
using System.Collections.Generic;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace Admin.Client
{
    public class TeleportCmd : BaseScript
    {
        public TeleportCmd()
        {
            API.RegisterCommand("tp", new Action<int, List<object>, string>(OnPlayerTpToCords), false);
        }
        
        private async void OnPlayerTpToCords(int p1, List<object> args, string p2)
        {
            // Jeśli w komendzie występują 3 argumenty to komenda tp wykonuje się z kordynatami x, y, z.
            if (args.Count == 3)
            {
                float posx;
                float posy;
                float posz;

                // Pobieranie ID gracza.
                int playerId = API.PlayerPedId();

                // Jeśli wartości nie są właściewe zwracany jest komunikat odnośnie poprawnego używania komendy.
                if (!float.TryParse(args[0].ToString(), out posx) || !float.TryParse(args[1].ToString(), out posy) || !float.TryParse(args[2].ToString(), out posz))
                {
                    TriggerEvent("chat:addMessage", new
                    {
                        args = new[] {$"^1[ADMIN]: ^0Poprawne użycie komenty to: /tp ^1pozycjaX pozycjaY (pozycjaZ)"}
                    });
                    return;
                }
                
                // Jeśli kolizja nie jest zrobiona w obrębie gracza to wykonuje się request w celu stworzenia kolizji na podanych kodrynatach.
                while (!API.HasCollisionLoadedAroundEntity(playerId))
                {
                    API.RequestCollisionAtCoord(posx, posy, posz);
                    await Delay(100);                   
                }
                
                //Funkcja odpowiedzialna za sam teleport oraz wiadomość zwrotną do gracza.
                API.SetEntityCoords(playerId, posx, posy, posz, false, false, false, true);
                TriggerEvent("chat:addMessage", new
                {
                    args = new[] {$"^1[ADMIN]: ^0Zostałeś przeniesiony na kordynaty: X:^1{posx}^0, Y:^1{posy}, ^0Z:^1{posz}"}
                });
            }
            
            //Jeśli w komendzie występują 2 argumenty to komenda tp wykonuje się z kordynatami x oraz y kordynat Z jest pobierany z podłoża
            else if (args.Count == 2)
            {

                float posx;
                float posy;
                float posz = 100.0f;
                
                // Pobieranie ID gracza.
                int playerId = API.PlayerPedId();
                
                // Jeśli wartości nie są właściewe zwracany jest komunikat odnośnie poprawnego używania komendy.
                if (!float.TryParse(args[0].ToString(), out posx) || !float.TryParse(args[1].ToString(), out posy))
                {
                    TriggerEvent("chat:addMessage", new
                    {
                        args = new[] {$"^1[ADMIN]: ^0Poprawne użycie komenty to: /tp ^1pozycjaX pozycjaY (pozycjaZ)"}
                    });
                    return;
                }

                // Pętla która wywołuje kolzicje na podanych przez nas kordynatach, oraz funckja odpwoiedzialna za pobieranie wysokości podłoża wymagane jest podówjne TP - Jest to błąd OneSynca
                do
                {
                    API.RequestCollisionAtCoord(posx, posy, posz);
                    API.GetGroundZFor_3dCoord(posx, posy, 1000.0f, ref posz, true);
                    API.SetEntityCoords(playerId, posx, posy, posz, false, false, false, true);
                    await Delay(100); 
                }while (posz == 0.0f);
                
                //Funkcja odpowiedzialna za sam teleport oraz wiadomość zwrotną do gracza.
                API.SetEntityCoords(playerId, posx, posy, posz, false, false, false, true);
                TriggerEvent("chat:addMessage", new
                {
                    args = new[] {$"^1[ADMIN]: ^0Zostałeś przeniesiony na kordynaty: X:^1{posx}^0, Y:^1{posy}, ^0Z:^1{posz}"}
                });
            }
            else
            {
                TriggerEvent("chat:addMessage", new
                {
                    args = new[] {$"^1[ADMIN]: ^0Poprawne użycie komenty to: /tp ^1pozycjaX pozycjaY (pozycjaZ)"}
                });
            }
        }
    }
}