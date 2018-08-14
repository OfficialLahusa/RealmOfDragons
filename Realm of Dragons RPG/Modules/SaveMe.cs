using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Realm_of_Dragons_RPG;

namespace Realm_of_Dragons_RPG.Modules
{
    public class SaveMe : ModuleBase<SocketCommandContext>
    {
        [Command("saveme")]
        public async Task SaveUser()
        {
            Serialization.UserAccount newAcc = Serialization.Serializer.loadUser(Context.User.Id);
            newAcc.userId = Context.User.Id;
            Serialization.Serializer.saveUser(newAcc);
        }
    }
}
