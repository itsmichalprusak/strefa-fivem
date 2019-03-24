using System.Collections.Generic;
using CitizenFX.Core.Native;

namespace Admin.Server.Helpers
{
    public static class NativeWrappers
    {
        public static void DropPlayer(string id, string reason)
        {
            Function.Call(Hash.DROP_PLAYER, id, reason);
        }
        
    }
}