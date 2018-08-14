using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realm_of_Dragons_RPG.Modules
{
    public class Embed : ModuleBase<SocketCommandContext>
    {
        [Command("embed")]
        public async Task EmbedAsync()
        {
            var eb = new EmbedBuilder()
            {
                Title = "Test Embed summoned by idk",
                Color = Color.Gold,
                Description = "Yeah this is a Test embed what did you expect ;)",
                ImageUrl = "https://climg6.bluestone.com/f_jpg,c_scale,w_1024,b_rgb:f0f0f0/giproduct/BIRS0139R73_YAA18XXXXXXXXXXXX_ABCD00-PICS-00001-1024-5449.png"
            };

            await ReplyAsync("", false, eb);
        }
    }
}
