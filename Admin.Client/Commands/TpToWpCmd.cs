using System;
using System.Collections.Generic;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace Admin.Client.Commands
{
    public class TpToWpCmd : BaseScript
    {
        public TpToWpCmd()
        {
            API.RegisterCommand("tptowp", new Action<int, List<object>, string>(OnAdminTpToWaypoint), false);
        }

        private async void OnAdminTpToWaypoint(int p1, List<object> args, string p2)
        {
            if (AdutyCmd.pAduty)
            {
                
                var waypoint = API.GetFirstBlipInfoId(8);
                Vector3 waypointPos = API.GetBlipCoords(waypoint);
                float waypointPosz = 100.0f;

                if (API.DoesBlipExist(waypoint))
                {
                    // Jeśli kolizja nie jest zrobiona w obrębie gracza to wykonuje się request w celu stworzenia kolizji na podanych kodrynatach.
                    while (!API.HasCollisionLoadedAroundEntity(API.PlayerPedId()))
                    {
                        API.RequestCollisionAtCoord(waypointPos.X, waypointPos.Y, waypointPosz);
                        await Delay(100);
                    } 
                    
                    // Pętla która wywołuje kolzicje na podanych przez nas kordynatach, oraz funckja odpwoiedzialna za pobieranie wysokości podłoża wymagane jest podówjne TP - Jest to błąd OneSynca
                    do
                    {
                        API.RequestCollisionAtCoord(waypointPos.X, waypointPos.Y, waypointPosz);
                        API.GetGroundZFor_3dCoord(waypointPos.X, waypointPos.Y, 1000.0f, ref waypointPosz, true);
                        API.SetEntityCoords(API.PlayerPedId(), waypointPos.X, waypointPos.Y, waypointPosz, false, false, false, true);
                        await Delay(100);
                    } while (waypointPosz == 0.0f);
                    
                    //Funkcja odpowiedzialna za sam teleport oraz wiadomość zwrotną do gracza.
                    API.SetEntityCoords(API.PlayerPedId(), waypointPos.X, waypointPos.Y, waypointPosz, false, false, false, true);
                    TriggerEvent("chat:addMessage", new
                    {
                        args = new[] {$"^1[ADMIN]: ^0Zostałeś przeniesiony do Waypointa ({waypointPos.X} | {waypointPos.Y} | {waypointPosz})"}
                    });
                }
                else
                {
                    TriggerEvent("chat:addMessage", new
                    {
                        args = new[] {$"^1[ADMIN]: ^0Nie znaleziono waypointa!"}
                    });
                }
            }
            
            else if (AdutyCmd.pAduty == false)
            {
                return;
            }
        }
    }
}