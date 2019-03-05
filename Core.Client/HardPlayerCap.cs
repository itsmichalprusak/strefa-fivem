using CitizenFX.Core;
using Core.Client.Utilities;
using System;
using System.Threading.Tasks;

namespace Core.Client
{
    public class HardPlayerCap : BaseScript
    {
        public HardPlayerCap()
        {
            BaseScript.Delay(1000);
            Debug.WriteLine("[Core] HardCap wystartował.");
            Tick += PlayerActivatedCheck;
        }

        public void RegisterEventHandler(string trigger, Delegate callback)
        {
            EventHandlers[trigger] += callback;
        }

        private async Task PlayerActivatedCheck()
        {
            if (NativeWrappers.NetworkIsSessionStarted())
            {
                TriggerServerEvent("HardPlayerCap.PlayerActivated");
                Tick -= PlayerActivatedCheck;
            }
            
            await Task.FromResult(0);
        }
    }
}