using System;
using System.Collections.Generic;
using System.Linq;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using static CitizenFX.Core.Native.API;

namespace Admin.Client
{
    public class ReviveCommand : BaseScript
    {
        public ReviveCommand()
        {
            EventHandlers["onClientResourceStart"] += new Action<string>(OnClientResourceStart);
        }

        private static void OnClientResourceStart(string resourceName)
        {
            API.RegisterCommand("revive", new Action<int, List<object>, string>((source, args, raw) =>
            {
                var position = Game.Player.Character.Position;
                NetworkResurrectLocalPlayer(
                    position.X, position.Y, position.Z, Game.Player.Character.Heading, false, false);
            }), false);
        }
    }
}