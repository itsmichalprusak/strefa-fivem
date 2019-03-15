using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitizenFX.Core;
using static System.Threading.Thread;
using static CitizenFX.Core.Native.API;

namespace Stasiek
{
    class Emotes : BaseScript
    {
        
        int currentProp;
        int[] lastProps = new int[255];

        public Emotes()
        {
            Tick += DeleteAllProps;
        }

        private async Task DeleteAllProps()
        {
            await Delay(1000 * 30);
            for(int i = 0; i < lastProps.Length; i++)
            {
                DeleteEntity(ref lastProps[i]);
            }
        }

        public void ClearPlayerEmotes()
        {
            ClearPedTasks(PlayerPedId());
            DetachEntity(currentProp, false, true);
            if (currentProp == 0) return;
            for(int i = 0; lastProps[i] == 0; i++)
            {
                if (lastProps[i] == 0)
                {
                    lastProps[i] = currentProp;
                    return;
                }
            }
        }

        public async Task PlayEmote(string lib, string anim, int flag)
        {
            if (!IsEntityPlayingAnim(PlayerPedId(), lib, anim, 3))
            {
                RequestAnimDict(lib);
                while (!HasAnimDictLoaded(lib)) await Delay(10);
                TaskPlayAnim(PlayerPedId(), lib, anim, 1.0f, 1.0f, -1, flag, 0f, false, false, false);
            }
            else
            {
                ClearPlayerEmotes();
            }
        }

        public async Task PlayEmote(string lib, string anim, int flag, string propName, int boneIndex)
        {
            if (!IsEntityPlayingAnim(PlayerPedId(), lib, anim, 3))
            {
                currentProp = CreateObject(GetHashKey(propName), 0f, 0f, 0f, true, true, true);
                AttachEntityToEntity(currentProp, PlayerPedId(), GetPedBoneIndex(PlayerPedId(), boneIndex), 0f, 0f, 0f, 0f, 0f, 0f, true, true, false, true, 1, true);
                RequestAnimDict(lib);
                while (!HasAnimDictLoaded(lib)) await Delay(10);
                TaskPlayAnim(PlayerPedId(), lib, anim, 1.0f, 1.0f, -1, flag, 0f, false, false, false);
            }
            else
            {
                ClearPlayerEmotes();
            }
        }

        //Metoda 3 => biblioteka animacji, animacja, flaga animacji, nazwa propa, index kości peda, pozycjaX, pozycjaY, pozycjaZ, rotacjaX, rotacjaY, rotacjaZ
        public async Task PlayEmote(string lib, string anim, int flag, string propName, int boneIndex, float px, float py, float pz, float rx, float ry, float rz)
        {
            if (!IsEntityPlayingAnim(PlayerPedId(), lib, anim, 3))
            {
                currentProp = CreateObject(GetHashKey(propName), 0f, 0f, 0f, true, true, true);
                AttachEntityToEntity(currentProp, PlayerPedId(), GetPedBoneIndex(PlayerPedId(), boneIndex), px, py, pz, rx, ry, rz, true, true, false, true, 1, true);
                RequestAnimDict(lib);
                while (!HasAnimDictLoaded(lib)) await Delay(10);
                TaskPlayAnim(PlayerPedId(), lib, anim, 1.0f, 1.0f, -1, flag, 0f, false, false, false);
            }
            else
            {
                ClearPlayerEmotes();
            }
        }
    }
}
