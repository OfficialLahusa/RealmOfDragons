using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realm_of_Dragons_RPG.Modules
{
    [Group("item")]
    public class Item : ModuleBase<SocketCommandContext>
    {
        private void LoopItemCollection(Serialization.ItemCollection coll, string namespace_prefix = "")
        {
            string modified_prefix = string.Empty;
            if(namespace_prefix == "")
            {
                modified_prefix = coll.name;
            } else
            {
                modified_prefix = namespace_prefix + "." + coll.name;
            }

            Console.WriteLine(modified_prefix + " (Items: " + coll.items.Count + ", Subcategories: " + coll.subcategories.Count + ")");

            foreach(Serialization.ItemCollection subCol in coll.subcategories)
            {
                LoopItemCollection(subCol, modified_prefix);
            }
            foreach(Serialization.ItemContainer item in coll.items)
            {
                Console.WriteLine(modified_prefix + "." + item.id);
            }
        }

        [Command("all"), Alias("showall")]
        public async Task ShowAllItems()
        {
            LoopItemCollection(Serialization.ItemHandler.items);
        }
    }
}
