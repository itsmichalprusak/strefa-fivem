using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace Core.Client
{
    public class NeverWanted : BaseScript
    {
        public NeverWanted()
        {
            Tick += timer;
            EventHandlers["onClientResourceStart"] += new Action<string>(OnClientResourceStart);
        }

        private void OnClientResourceStart(string resourceName)
        {
            if (GetCurrentResourceName() != resourceName) return;
        }

        private async Task timer()
        {
            // Jesli Wanted Jest wiekszy badz rowna sie 0 to
            if (GetPlayerWantedLevel(PlayerId()) >= 0)
            {
                //Ustawia Wanted na 0
                SetPlayerWantedLevel(PlayerId(), 0, false);
                SetPlayerWantedLevelNow(PlayerId(), false);
            }

            await Delay(1000);
        }
    }
}