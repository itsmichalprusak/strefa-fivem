using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using static CitizenFX.Core.Native.API;


namespace Animations
{
    public class Animations : BaseScript
    {
        public Animations()
        {
            Tick += ButtonCheck;
        }

        private async Task ButtonCheck()
        {
            if (OnKeyUp(73))
            {
                ClearPlayerPedTasks(false);
            }

            if (OnKeyUp(289))
            {
                PlayAnimationOnPlayerPed("missminuteman_1ig_2", "handsup_enter", 50);
            }
            else if (OnKeyUp(47))
            {
                PlayAnimationOnPlayerPed("amb@world_human_hang_out_street@female_arms_crossed@enter", "enter", 50);
            }

            await Task.FromResult(0);
        }

        private async void PlayAnimationOnPlayerPed(string lib, string anim, int flag)
        {
            if (!IsMyPedPlayingAnimation(lib, anim))
            {
                Function.Call(Hash.REQUEST_ANIM_DICT, lib);
                while (!Function.Call<bool>(Hash.HAS_ANIM_DICT_LOADED, lib)) await Delay(1);
                Function.Call(Hash.TASK_PLAY_ANIM, Game.PlayerPed, lib, anim, 1.0, 1.0, -1, flag, 0, 0, 0, 0);
            }
            else
            {
                ClearPlayerPedTasks(false);
            }
        }

        private void ClearPlayerPedTasks(bool immediately)
        {
            if (immediately)
                Function.Call(Hash.CLEAR_PED_TASKS_IMMEDIATELY, Game.PlayerPed);
            else
                Function.Call(Hash.CLEAR_PED_TASKS, Game.PlayerPed);
        }

        private bool IsMyPedPlayingAnimation(string lib, string anim)
        {
            if (Function.Call<bool>(Hash.IS_ENTITY_PLAYING_ANIM, Game.PlayerPed, lib, anim, 3))
                return true;
            else
                return false;
        }

        private bool OnKeyUp(int key)
        {
            if (IsControlJustReleased(0, key))
                return true;
            else
                return false;
        }
    }
    
}
