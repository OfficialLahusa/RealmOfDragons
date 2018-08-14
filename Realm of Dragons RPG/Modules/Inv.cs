using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Realm_of_Dragons_RPG;

namespace Realm_of_Dragons_RPG.Modules
{
    [Group("inv")]
    public class Inv : ModuleBase<SocketCommandContext>
    {
        [Command, Alias("show")]
        public async Task ShowInventory(uint page = 0)
        {
            await ReplyAsync($"Showing Inventory Page {page}", false);
        }
        [Group("item")]
        public class Item : ModuleBase<SocketCommandContext>
        {
            [Command]
            public async Task ShowItem(uint id = 0)
            {
                await ReplyAsync($"The Item View functionality is currently not available, contact the dev for up-to-date information (Passed ID: {id})", false);
            }
        }
    }
}
