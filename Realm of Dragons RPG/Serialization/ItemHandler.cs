using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Realm_of_Dragons_RPG.Serialization
{
    public enum ItemRarity
    {
        [XmlEnum]
        Common,
        [XmlEnum]
        Uncommon,
        [XmlEnum]
        Rare,
        [XmlEnum]
        Epic,
        [XmlEnum]
        Legendary,
        [XmlEnum]
        Quest
    }

    [XmlRoot("item")]
    public class ItemContainer
    {
        [XmlAttribute("name")]
        public string name = "Unnamed Item";
        [XmlAttribute("id")]
        public string id = "unnamed_item";
        [XmlText]
        public string description = "This item has no description";
        [XmlAttribute]
        public ItemRarity rarity = ItemRarity.Common;
    }

    [XmlRoot("category")]
    public class ItemCollection
    {
        [XmlAttribute]
        public string name = "item";
        [XmlArray]
        public List<ItemCollection> subcategories = new List<ItemCollection>();
        [XmlArray]
        public List<ItemContainer> items = new List<ItemContainer>();
    }

    public static class ItemHandler
    {
        static ItemHandler()
        {
            items = Serializer.loadCollection("items");
        }
        public static ItemCollection items = new ItemCollection();
    }
}
