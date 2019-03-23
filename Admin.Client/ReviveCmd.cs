using System;
using System.Collections.Generic;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace Admin.Client
{
    public class ReviveCmd : BaseScript
    {
        public ReviveCmd()
        {
            EventHandlers["onClientResourceStart"] += new Action<string>(OnClientResourceStart);
        }

        private static void OnClientResourceStart(string resourceName)
        {
            RegisterCommand("revive", new Action<int, List<object>, string>((source, args, raw) =>
            {
                var position = Game.Player.Character.Position;
                NetworkResurrectLocalPlayer(
                    position.X, position.Y, position.Z, Game.Player.Character.Heading, false, false);
            }), false);
        }
    }
}