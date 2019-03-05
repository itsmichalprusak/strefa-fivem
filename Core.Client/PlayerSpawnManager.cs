using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using Core.Client.Utilities;
using System;
using System.Threading.Tasks;

namespace Core.Client
{
    public class PlayerSpawnManager : BaseScript
    {
        // TODO: To pójdzie do przeróbki - trzeba ogarnąć respawn inaczej.
        private readonly Vector3 _initialSpawnCoordinates 
            = new Vector3(
                (float) -802.311, 
                (float) 175.056, 
                (float) 72.8446
            );
        private bool _hasSpawned = false;
        private readonly Model _defaultPlayerModel = PedHash.Airhostess01SFY;

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

        private async void SpawnPlayer(Model model, Vector3 location, float heading)
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