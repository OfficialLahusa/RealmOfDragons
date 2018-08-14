using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Realm_of_Dragons_RPG;

namespace Realm_of_Dragons_RPG.Modules
{
    public class Start : ModuleBase<SocketCommandContext>
    {
        [Command("start")]
        public async Task StartAsync()
        {
            Serialization.UserAccount newAcc = Serialization.Serializer.loadUser(Context.User.Id);
            newAcc.userId = Context.User.Id;
            Serialization.Serializer.saveUser(newAcc);
            await ReplyAsync("RPG started for User " + Context.User.Mention);
        }
    }
}
