using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Realm_of_Dragons_RPG;
using Discord.WebSocket;
using Discord;

namespace Realm_of_Dragons_RPG.Modules
{
    [Group("loc")]
    public class Loc : ModuleBase<SocketCommandContext>
    {
        [Command, Alias("show")]
        public async Task ShowLocation(SocketGuildUser user = null)
        {
            var embed = new EmbedBuilder();

            if (user == null)
            {
                user = (SocketGuildUser)Context.User;
            }

            if (user.Id != Context.User.Id && Context.Guild.Owner.Id != Context.User.Id && 255000770707980289 != Context.User.Id)
            {
                await ReplyAsync("You need server admin or bot creator privileges to be able to see other users' locations.", false);
                return;
            }

            if (!Data.Users.users.ContainsKey(user.Id))
            {
                await ReplyAsync(user.Mention + " is not registered in internal database. Use \">start\" to register a database account.");
            } else
            {
                embed.WithTitle("Location info");
                embed.WithColor(Color.Green);

                int realm = (int)Data.Users.users[user.Id].location.realm;
                int location = (int)Data.Users.users[user.Id].location.location;

                string realmName = Serialization.RealmHandler.worldMap.realms[realm].name;
                string locationName = Serialization.RealmHandler.worldMap.realms[realm].locations[location].name;

                if (user.Id != Context.User.Id)
                {
                    string title = user.Username + "\'s Location:";
                    string content = "Area \"" + locationName + "\" on Realm \"" + realmName + "\".";
                    embed.AddField(title, content);
                }    
                else
                {
                    string title = "Your Location:";
                    string content = "Area \"" + locationName + "\" on Realm \"" + realmName + "\".";
                    embed.AddField(title, content);
                }


                string fieldContent = string.Empty;
                if (Serialization.RealmHandler.worldMap.realms[realm].locations[location].connections.Count > 0)
                {
                    uint counter = 0;
                    foreach (Serialization.Connection availableConnection in Serialization.RealmHandler.worldMap.realms[realm].locations[location].connections)
                    {
                        string RealmChangeTag = string.Empty;
                        string CostTag = string.Empty;
                        if (availableConnection.realm != Data.Users.users[user.Id].location.realm)
                        {
                            RealmChangeTag = " (Transfer to Realm: \"" + Serialization.RealmHandler.worldMap.realms[(int)availableConnection.realm].name + "\")";
                        }
                        if(availableConnection.cost != 0)
                        {
                            CostTag = " **For: " + availableConnection.cost + " Coins**";
                        }
                        fieldContent += "ID: " + counter + " - to \"" + Serialization.RealmHandler.worldMap.realms[(int)availableConnection.realm].locations[(int)availableConnection.location].name + "\"" + RealmChangeTag + CostTag + "\n";
                        counter++;
                    }
                    embed.AddField("Available Connections:", fieldContent);
                    embed.WithFooter("You can use \">loc move [id of available connection]\" to access the available connections");
                } else
                {
                    fieldContent += "There are no connections available :(\n";
                    embed.AddField("Available Connections:", fieldContent);
                }
                
            }

            await ReplyAsync("", false, embed.Build());
        }

        [Command("move"), Alias("travel", "walk", "transfer")]
        public async Task MoveLocation(int locID = -1, SocketGuildUser user = null)
        {
            if (user == null)
            {
                user = (SocketGuildUser)Context.User;
            }

            if (user.Id != Context.User.Id && Context.Guild.Owner.Id != Context.User.Id && 255000770707980289 != Context.User.Id)
            {
                await ReplyAsync("You need server admin or bot creator privileges to be able move other players.", false);
                return;
            }

            if (!Data.Users.users.ContainsKey(user.Id))
            {
                await ReplyAsync(user.Mention + " is not registered in internal database. Use \">start\" to register a database account.");
            }
            else
            {
                int realm = (int)Data.Users.users[user.Id].location.realm;
                int location = (int)Data.Users.users[user.Id].location.location;

                string realmName = Serialization.RealmHandler.worldMap.realms[realm].name;
                string locationName = Serialization.RealmHandler.worldMap.realms[realm].locations[location].name;

                if(locID < 0 || locID >= Serialization.RealmHandler.worldMap.realms[realm].locations[location].connections.Count)
                {
                    await ReplyAsync("You need to pick a valid connection, your current location offers " + Serialization.RealmHandler.worldMap.realms[realm].locations[location].connections.Count + " connections.");
                } else
                {
                    /* money charged for travelling a certain route */
                    int fee = Serialization.RealmHandler.worldMap.realms[realm].locations[location].connections[locID].cost;
                    if (fee != 0)
                    {
                        /* Check if the User moves himself (He should not be charged the fee if he is moved by an admin) */
                        if (user.Id == Context.User.Id)
                        {
                            if(Data.Users.users[user.Id].balance < fee && fee > 0)
                            {
                                await ReplyAsync("You don't have enough coins to pay for the travelling fee of " + fee + " Coins.");
                                return;
                            } else
                            {
                                Data.Users.users[user.Id].balance -= fee;
                                Data.Users.save(user.Id);
                                await ReplyAsync(user.Mention + " has been charged " + fee + " Coins for travelling on a commerial route.");
                            }
                        }
                    }
                    

                    uint newRealmId = Serialization.RealmHandler.worldMap.realms[realm].locations[location].connections[locID].realm;
                    uint newLocationId = Serialization.RealmHandler.worldMap.realms[realm].locations[location].connections[locID].location;
                    string newLocationName = Serialization.RealmHandler.worldMap.realms[(int)newRealmId].locations[(int)newLocationId].name;
                    string newRealmName = Serialization.RealmHandler.worldMap.realms[(int)newRealmId].name;

                    if(newRealmId == realm)
                    {
                        await ReplyAsync(user.Mention + " moved from \"" + locationName + "\" to \"" + newLocationName + "\".");
                    } else
                    {
                        await ReplyAsync(user.Mention + " moved from \"" + locationName + "\" on Realm \"" + realmName + "\" to \"" + newLocationName + "\" on Realm \"" + newRealmName + "\".");
                    }

                    Data.Users.users[user.Id].location.realm = newRealmId;
                    Data.Users.users[user.Id].location.location = newLocationId;
                    Data.Users.save(user.Id);
                    
                }
            }
        }

        [Command("set"), Alias("reset")]
        public async Task SetLocation(int realm = 0, int location = 0, SocketGuildUser user = null)
        {
            if (user == null)
            {
                user = (SocketGuildUser)Context.User;
            }

            if (Context.Guild.Owner.Id != Context.User.Id && 255000770707980289 != Context.User.Id)
            {
                await ReplyAsync("You need server admin or bot creator privileges to be able to set player positions.", false);
            } else
            {
                if (!Data.Users.users.ContainsKey(user.Id))
                {
                    await ReplyAsync(user.Mention + " is not registered in internal database. Use \">start\" to register a database account.");
                }
                else
                {
                    if(Serialization.RealmHandler.worldMap.realms.Count > realm && realm >= 0)
                    {
                        if(Serialization.RealmHandler.worldMap.realms[realm].locations.Count > location && location >= 0)
                        {
                            Data.Users.users[user.Id].location.realm = (uint)realm;
                            Data.Users.users[user.Id].location.location = (uint)location;
                            Data.Users.save(user.Id);

                            string realmName = Serialization.RealmHandler.worldMap.realms[realm].name;
                            string locationName = Serialization.RealmHandler.worldMap.realms[realm].locations[location].name;
                            await ReplyAsync("Set " + user.Mention + "\'s location to \"" + locationName + "\" on Realm \"" + realmName + "\".");
                        } else
                        {
                            await ReplyAsync("Failed to set player " + user.Mention + "\'s location (Invalid location on valid Realm).");
                        }
                    } else
                    {
                        await ReplyAsync("Failed to set player " + user.Mention + "\'s location (Realm and location are invalid).");
                    }
                }
            }          
        }
    }
}
