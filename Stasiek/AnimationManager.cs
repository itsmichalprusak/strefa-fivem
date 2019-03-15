using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitizenFX.Core;
using Newtonsoft.Json;
using static CitizenFX.Core.Native.API;

namespace Stasiek
{
    public class AnimationManager : BaseScript
    {
        Emotes emotes = new Emotes();
        public static List<EmoteCommand> EmoteCommandList { get; set; }

        public class EmoteCommand
        {
            public string command { get; set; } = "";
            public string lib { get; set; } = "";
            public string anim { get; set; } = "";
            public int flag { get; set; } = 0;
            public string propName { get; set; } = "";
            public int boneIndex { get; set; } = 0;
            public float px { get; set; } = 0;
            public float py { get; set; } = 0;
            public float pz { get; set; } = 0;
            public float rx { get; set; } = 0;
            public float ry { get; set; } = 0;
            public float rz { get; set; } = 0;
        }

        public AnimationManager()
        {
            Tick += Update;
            EventHandlers["onClientResourceStart"] += new Action(LoadJsonCommands);
        }

        private void LoadJsonCommands()
        {
            EmoteCommandList = JsonConvert.DeserializeObject<List<EmoteCommand>>(LoadResourceFile(GetCurrentResourceName(), "AnimationManagerConfig.json") ?? "{}");
        }
  
        private async Task Update()
        {
            if (IsControlJustReleased(0, 73))
            {
                emotes.ClearPlayerEmotes();
            }else if (IsControlJustReleased(0, 47))
            {
                await emotes.PlayEmote("amb@world_human_hang_out_street@female_arms_crossed@enter", "enter", 50);
            }
            else if (IsControlJustReleased(0, 289))
            {
                await emotes.PlayEmote("missminuteman_1ig_2", "handsup_enter", 50);
            }

            RegisterCommand("e", new Action<int, List<object>, string>(async(source, args, raw) => {
                if (args.Count() > 0)
                {
                    var animation = EmoteCommandList.FirstOrDefault(c => c.command == args[0].ToString());
                    if (animation == null) return;
                    if (animation.propName != "")
                    {
                        await emotes.PlayEmote(animation.lib, animation.anim, animation.flag, animation.propName, animation.boneIndex, animation.px, animation.py, animation.pz, animation.rx, animation.ry, animation.rz);
                    }
                    else
                    {
                        await emotes.PlayEmote(animation.lib, animation.anim, animation.flag);
                    }
                }
            }), false);
            await Task.FromResult(0);
        }
    }
}
