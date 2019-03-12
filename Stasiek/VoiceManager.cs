using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using static CitizenFX.Core.Native.API;


namespace Stasiek
{
    public class VoiceManager : BaseScript
    {
        float[] voice = new float[] { 2.0f, 12.0f, 40.0f };
        int v = 0;
        bool debug = true;
        public VoiceManager()
        {
            Tick += ChangeVoicePromixy;
        }

        private async Task ChangeVoicePromixy()
        {
            if (IsControlJustReleased(0, 166))
            {
                if (v == 2)
                {
                    v = -1;
                }
                v++;
                NetworkSetTalkerProximity(voice[v]);
            }
            if (debug)
            {
                Vector3 coords = GetEntityCoords(PlayerPedId(), true);
                DisplayDebug(coords.X, coords.Y, coords.Z, voice[v]);
            }
            RegisterCommand("voicemanager_debug", new Action<int, List<object>, string>(async (source, args, raw) =>{ debug = !debug; }), false);

            await Task.FromResult(0);
        }

        private void SetDefaultPromixy()
        {
            NetworkSetTalkerProximity(voice[v]);
        }

        private void DisplayDebug(float x, float y, float z, float s)
        {
            DrawMarker(28, x, y, z, 0.0f, 0.0f, 0.0f, 0, 0.0f, 0.0f, s, s, s, 0, 255, 0, 30 * (v + 1), false, true, 2, false, null, null, false);
        }
    }

}
