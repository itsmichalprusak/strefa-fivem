using CitizenFX.Core;
using CitizenFX.Core.UI;
using Core.Client.Utilities;
using System;
using System.Threading.Tasks;

namespace Core.Client
{
    /// <summary>
    /// Standalone'owy skrypt zarządzający spawnowaniem graczy, będący zamiennikiem FiveMowskiego w Lua.
    /// </summary>
    public class PlayerSpawnManager : BaseScript
    {
        // TODO: To pójdzie do przeróbki - trzeba ogarnąć respawn inaczej.
        private readonly Vector3 _initialSpawnCoordinates 
            = new Vector3(
                (float) -802.311, 
                (float) 175.056, 
                (float) 72.8446
            );
        private bool _hasSpawned;
        private readonly Model _defaultPlayerModel = PedHash.Airhostess01SFY;

        /// <summary>
        /// Konstruktor, wywoływany przy zinstancjonowaniu skryptu.
        /// </summary>
        public PlayerSpawnManager()
        {
            Tick += SpawnCheck;
            Debug.WriteLine("[Core] SpawnManager wystartował.");
            Exports.Add("hasSpawned", new Func<bool>(() => _hasSpawned));
        }

        public void RegisterEventHandler(string trigger, Delegate callback)
        {
            EventHandlers[trigger] += callback;
        }

        /// <summary>
        /// Sprawdza, czy gracz został poprawnie zespawnowany. Jeśli nie, spawnuje go na nowo.
        /// </summary>
        /// <returns>
        /// Wywołane zadanie (Task).
        /// </returns>
        private async Task SpawnCheck()
        {
            var playerPedExists = (Game.PlayerPed.Handle != 0);
            var playerActive = NativeWrappers.NetworkIsPlayerActive(NativeWrappers.PlayerId());

            if (playerPedExists && playerActive && !_hasSpawned)
            {
                SpawnPlayer(_defaultPlayerModel, _initialSpawnCoordinates, (float)0.0);
                _hasSpawned = true;
            }
            
            await Task.FromResult(0);
        }

        /// <summary>
        /// Właściwie spawnuje gracza i usuwa ekran ładowania.
        /// </summary>
        /// <remarks>
        /// Wywołuje event <c>playerSpawned</c>, gdy wszystko dobiegnie końca.
        /// </remarks>
        /// <param name="model">Typ modelu, z jakim zespawnować gracza.</param>
        /// <param name="location">Dokładna wektorowa lokalizacja spawnu gracza.</param>
        /// <param name="heading">W jakim kierunku gracz ma być ustawiony przodem.</param>
        private static async void SpawnPlayer(Model model, Vector3 location, float heading)
        {
            // FiveM używa tego w swoim kodzie, po stronie C++ - dlatego używamy tego tu, dla zgodności.
            Screen.Fading.FadeIn(0);
            
            NativeWrappers.RequestCollisionAtCoord(location);
            
            await Game.Player.ChangeModel(model);
            Game.PlayerPed.Position = location;
            Game.PlayerPed.Heading = heading;
            
            NativeWrappers.ShutdownLoadingScreen();
            TriggerEvent("playerSpawned");
        }
    }
}