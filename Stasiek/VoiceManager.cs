using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;


namespace Stasiek
{
    public class VoiceManager : BaseScript
    {
        /// Tablica gdzie można dodawać dowolną liczbę ustawień promixy dźwięku
        float[] voice = new float[] { 2.0f, 12.0f, 40.0f };
        string[] voiceLabel = new string[] { "Szept", "Normalny", "Krzyk"};
        int v = 0;
        bool debug = false;

        public VoiceManager()
        {
            Tick += ChangeVoicePromixy;
            EventHandlers["onClientResourceStart"] += new Action<string>(OnClientResourceStart);
        }

        private void OnClientResourceStart(string resourceName)
        {
            if (GetCurrentResourceName() != resourceName) return;
            RegisterCommand("voicemanager_debug", new Action<int, List<object>, string>((source, args, raw) => {
                debug = !debug;
                Debug.Write("Stasiek.VoiceManager => Debugger " + debug);
            }), false);
        }

        private async Task ChangeVoicePromixy()
        {
            //Zmiana dźwięku [F5]
            if (IsControlJustReleased(0, 166))
            {
                if (v == voice.Length - 1)
                {
                    v = -1;
                }
                v++;
                NetworkSetTalkerProximity(voice[v]);
                Debug.Write("Stasiek.VoiceManager => Zmieniono zasieg Voice'a z " + NetworkGetTalkerProximity() + " na " + voiceLabel[v]);
            }
            //Debuger pokazuje Sphere który oznajmia zasięg dźwięku
            if (debug)
            {
                Vector3 coords = GetEntityCoords(PlayerPedId(), true);
                DisplayDebug(coords.X, coords.Y, coords.Z, voice[v]);
            }

            await Task.FromResult(0);
        }

        //Ustawiania promixy(zasięgu) voica
        private void SetDefaultPromixy()
        {
            NetworkSetTalkerProximity(voice[v]);
        }

        //Wyświetlanie Sphere dla debugera
        private void DisplayDebug(float x, float y, float z, float s)
        {
            DrawMarker(28, x, y, z, 0.0f, 0.0f, 0.0f, 0, 0.0f, 0.0f, s, s, s, 0, 255, 0, 30 * (v + 1), false, true, 2, false, null, null, false);
        }
    }

}
