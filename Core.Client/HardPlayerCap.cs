using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using Core.Client.Utilities;

namespace Core.Client
{
    /// <summary>
    ///     Standalone'owy skrypt pomagający w utrzymaniu capa 64 graczy, będący zamiennikiem FiveMowskiego w Lua.
    /// </summary>
    public class HardPlayerCap : BaseScript
    {
        /// <summary>
        ///     Konstruktor, wywoływany przy zinstancjonowaniu skryptu.
        /// </summary>
        public HardPlayerCap()
        {
            Delay(1000);
            Debug.WriteLine("[Core] HardCap wystartował.");
            Tick += PlayerActivatedCheck;
        }

        public void RegisterEventHandler(string trigger, Delegate callback)
        {
            EventHandlers[trigger] += callback;
        }

        /// <summary>
        ///     Sprawdza, czy gracz został poprawnie zaktywowany sieciowo i dołączony do serwera.
        /// </summary>
        /// <returns>
        ///     Wywołane zadanie (Task).
        /// </returns>
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