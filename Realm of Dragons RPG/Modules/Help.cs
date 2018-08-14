using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realm_of_Dragons_RPG.Modules
{
    [Group("help")]
    public class Help : ModuleBase<SocketCommandContext>
    {
        [Command]
        public async Task HelpOverview()
        {
            EmbedBuilder helpOverviewEmbed = new EmbedBuilder();
            string helpLines = "To specify which help you want to access, use the following commands:\n";
            helpLines += "\">help general\" for general help\n";
            helpLines += "\">help trade\" for trade help\n";
            helpLines += "\">help loc\" for location and walking\n";
            helpLines += "\">help inv\" for inventory help\n";
            helpLines += "\">help quest\" for quest help\n";
            helpLines += "\">help map\" for map help\n";
            helpOverviewEmbed.AddField("Help Overview", helpLines);
            await ReplyAsync("", false, helpOverviewEmbed.Build());
        }

        [Command("general")]
        public async Task GeneralHelp()
        {
            EmbedBuilder tradeHelp = new EmbedBuilder();
            string helpLines = "Uncathegorized but sometimes useful commands:\n";
            helpLines += "\">bal\" shows your current coin balance\n";
            helpLines += "\">start\" adds you to the system and starts the RPG for you\n";
            tradeHelp.AddField("Trade Help", helpLines);
            await ReplyAsync("", false, tradeHelp.Build());
        }

        [Command("trade")]
        public async Task TradeHelp()
        {
            EmbedBuilder tradeHelp = new EmbedBuilder();
            string helpLines = "The \">trade\" command allows for the following variations:\n";
            helpLines += "\">trade @User#1234\" requests a safe trade with a the specified user\n";
            helpLines += "\">trade accept\" accepts a pending trade request\n";
            helpLines += "\">trade deny\" denies a pending trade request\n";
            helpLines += "\">trade add [item's slot] [optional: amount]\" adds an item into a running trade\n";
            helpLines += "\">trade rem [item's position in trade window]\" removes an item from a running trade\n";
            helpLines += "\">trade credits [credit amount]\" sets the amount of credits you want to trade\n";
            helpLines += "\">trade confirm\" confirms the trade (both players need to confirm for completion)\n";
            helpLines += "\">trade leave\" leaves the trade, all items and credits are returned\n";
            tradeHelp.AddField("Trade Help", helpLines);
            await ReplyAsync("", false, tradeHelp.Build());
        }

        [Command("loc")]
        public async Task LocHelp()
        {
            EmbedBuilder tradeHelp = new EmbedBuilder();
            string helpLines = "The \">loc\" command allows for the following variations:\n";
            helpLines += "\">loc\" shows your current location and all available connections\n";
            helpLines += "\">loc move [id of available connection]\" moves you to a connected location (fees may be charged on commercial routes)\n";
            tradeHelp.AddField("Trade Help", helpLines);
            await ReplyAsync("", false, tradeHelp.Build());
        }
    }
}
