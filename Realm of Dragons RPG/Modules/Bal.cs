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
    [Group("bal"), Alias("money", "coins")]
    public class Bal : ModuleBase<SocketCommandContext>
    {
        [Command]
        public async Task ShowBalance(SocketGuildUser targetUser = null)
        {
            if (targetUser == null) targetUser = (SocketGuildUser)Context.User;
            ulong id = targetUser.Id;
            if (id != Context.User.Id && Context.Guild.Owner.Id != Context.User.Id && 255000770707980289 != Context.User.Id)
            {
                await ReplyAsync("You need server admin or bot creator privileges to be able to view other balances.", false);
                return;
            }

            Data.Users.refresh(id);
            
            if(!Data.Users.users.ContainsKey(id))
            {
                await ReplyAsync(targetUser.Mention + " is not registered in internal database.");
            } else
            {
                await ReplyAsync(targetUser.Mention + "'s balance: " + Data.Users.users[id].balance + " Coins.", false);
            }
        }

        [Command("add")]
        public async Task AddBalance(long change = 0, SocketGuildUser targetUser = null)
        {
            if (targetUser == null) targetUser = (SocketGuildUser)Context.User;

            if(Context.Guild.Owner.Id != Context.User.Id && 255000770707980289 != Context.User.Id)
            {
                await ReplyAsync("You need server admin or bot creator privileges to be able to change balance.");
                return;
            } else
            {
                Data.Users.refresh(targetUser.Id);
                if (!Data.Users.users.ContainsKey(targetUser.Id))
                {
                    await ReplyAsync(targetUser.Mention + " is not registered in internal database.");
                } else
                {
                    Data.Users.users[targetUser.Id].balance += change;
                    char sign = '+';
                    if (change == 0) sign = '±';
                    if (change < 0) sign = '-';
                    Data.Users.save(targetUser.Id);
                    await ReplyAsync("Saved " + targetUser.Mention + "'s balance, it is currently " + Data.Users.users[targetUser.Id].balance + " Coins (changed by " + sign + change + ").");
                }
            }
        }

        [Command("rem")]
        public async Task RemBalance(long change = 0, SocketGuildUser targetUser = null)
        {
            if (targetUser == null) targetUser = (SocketGuildUser)Context.User;

            if (Context.Guild.Owner.Id != Context.User.Id && 255000770707980289 != Context.User.Id)
            {
                await ReplyAsync("You need server admin or bot creator privileges to be able to change balance.");
                return;
            }
            else
            {
                Data.Users.refresh(targetUser.Id);
                if (!Data.Users.users.ContainsKey(targetUser.Id))
                {
                    await ReplyAsync(targetUser.Mention + " is not registered in internal database.");
                }
                else
                {
                    Data.Users.users[targetUser.Id].balance -= change;
                    char sign = '-';
                    if (change == 0) sign = '±';
                    if (change < 0) sign = '+';
                    Data.Users.save(targetUser.Id);
                    await ReplyAsync("Saved " + targetUser.Mention + "'s balance, it is currently " + Data.Users.users[targetUser.Id].balance + " Coins (changed by " + sign + change + ").");
                }
            }
        }


        [Command("set")]
        public async Task SetBalance(long val = 0, SocketGuildUser targetUser = null)
        {
            if (targetUser == null) targetUser = (SocketGuildUser)Context.User;

            if (Context.Guild.Owner.Id != Context.User.Id && 255000770707980289 != Context.User.Id)
            {
                await ReplyAsync("You need server admin or bot creator privileges to be able to change balance.");
                return;
            }
            else
            {
                Serialization.UserAccount user = Serialization.Serializer.loadUser(targetUser.Id);
                if (user.userId == 0)
                {
                    await ReplyAsync(targetUser.Mention + " is not registered in internal database.");
                }
                else
                {
                    Data.Users.users[user.userId].balance = val;
                    Data.Users.save(user.userId);
                    await ReplyAsync("Saved " + targetUser.Mention + "'s balance, it is currently " + Data.Users.users[user.userId].balance + " Coins.");
                    return;
                }
            }
        }
    }
}
