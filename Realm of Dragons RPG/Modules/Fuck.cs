using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Realm_of_Dragons_RPG;

namespace Realm_of_Dragons_RPG.Modules
{
    [Group("fuck"), Alias("noob", "dick", "cunt", "suck", "ligma", "hure", "nab", "shit", "idiot", "stupid", "stfu")]
    public class Fuck : ModuleBase<SocketCommandContext>
    {
        [Command]
        public async Task FuckPlayer()
        {
            SocketGuildUser targetUser = (SocketGuildUser)Context.User;
            await ReplyAsync("The RPG bot is not pleased by your use of the english language, your balance is reduced by 100.");

            {
                Serialization.UserAccount user = Serialization.Serializer.loadUser(targetUser.Id);
                if (user.userId == 0)
                {
                    await ReplyAsync(targetUser.Mention + " is not registered in internal database.");
                }
                else
                {
                    Data.Users.users[targetUser.Id].balance -= 100;
                    if (Data.Users.users[targetUser.Id].balance < 0) Data.Users.users[targetUser.Id].balance = 0;
                    Data.Users.save(targetUser.Id);
                    await ReplyAsync("Saved " + targetUser.Mention + "'s balance, it is currently " + user.balance + " Coins (changed by -100).");
                    return;
                }
            }

            
        }
    }
}
