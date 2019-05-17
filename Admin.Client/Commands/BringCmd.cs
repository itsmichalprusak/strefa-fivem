using System;
using System.Collections.Generic;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace Admin.Client.Commands
{
    public class BringCmd : BaseScript
    {

        public BringCmd()
        {
            API.RegisterCommand("bring", new Action<int, List<object>, string>(OnAdminBring), false);
        }

        private async void OnAdminBring(int p1, List<object> args, string p2)
        {
            if (AdutyCmd.pAduty)
            {
                if (!int.TryParse(args[0].ToString(), out int targetid)) return;
                var playerId = API.GetPlayerFromServerId(targetid);
                var online = API.NetworkIsPlayerActive(playerId);
                Vector3 adminCords = API.GetEntityCoords(API.PlayerPedId(), true);

                if (!online)
                {
                    TriggerEvent("chat:addMessage", new
                    {
                        args = new[] {$"^1[ADMIN]: ^0Gracz o [ID:^1{targetid}^0] jest offline!"}
                    });
                }
                else
                {
                    //API.SetEntityCoords(target, adminCords.X, adminCords.Y, adminCords.Z, false, false, false, true);
                    Debug.WriteLine($"ID: {targetid} was teleported to: {adminCords}");
                }
            }
        }
    }
}