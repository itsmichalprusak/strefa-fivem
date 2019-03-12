using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using static CitizenFX.Core.Native.API;


namespace Stasiek
{
    public class Stasiek : BaseScript
    {
        public Stasiek()
        {
            Tick += ButtonCheck;
        }

        private async Task ButtonCheck()
        {
            //[X] => Usuwa wszystkie Taski z Peda czyli anuluje animacje itd.
            if (IsControlJustReleased(0, 73))
            {
                ClearPedTasks(PlayerPedId());
            }

            //[F2] => Animacja podnoszenia rąk.
            if (IsControlJustReleased(0, 289))
            {
                PlayAnimationOnPlayerPed("missminuteman_1ig_2", "handsup_enter", 50);
            }
            //[G] => Animacja założenia rąk.
            else if (IsControlJustReleased(0, 47))
            {
                PlayAnimationOnPlayerPed("amb@world_human_hang_out_street@female_arms_crossed@enter", "enter", 50);
            }

            //[B] => Animacja pokazywanie palcem.
            if (IsControlJustReleased(0, 29))
            {
                RequestAnimDict("anim@mp_point");
                while (!HasAnimDictLoaded("anim@mp_point")) await Delay(1);
                SetPedCurrentWeaponVisible(PlayerPedId(), false, true, true, true);
                SetPedConfigFlag(PlayerPedId(), 36, true);
                TaskMoveNetwork(PlayerPedId(), "task_mp_pointing", 0.5f, false, "anim@mp_point", 24);
                RemoveAnimDict("anim@mp_point");
            }

            await Task.FromResult(0);
        }

        //Funkcja uruchamiająca animację
        private async void PlayAnimationOnPlayerPed(string lib, string anim, int flag)
        {
            //Jeżelu nie gra tej animacji to niech zacznie
            if (!IsEntityPlayingAnim(PlayerPedId(), lib, anim, 3))
            {
                RequestAnimDict(lib);
                while (!HasAnimDictLoaded(lib)) await Delay(1);
                TaskPlayAnim(PlayerPedId(), lib, anim, 1.0f, 1.0f, -1, flag, 0f, false, false, false);
            }
            //W przeciwnym wypadku jeżeli już gra tę animację to czyścimy Ped'owi aktualne Taski
            else
            {
                ClearPedTasks(PlayerPedId());
            }
        }
    }
    
}
