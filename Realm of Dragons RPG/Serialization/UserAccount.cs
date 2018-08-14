using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Realm_of_Dragons_RPG.Serialization
{
    [XmlRoot("ItemEntry")]
    public class ItemEntry
    {
        public ItemEntry() { }
        public ItemEntry(uint newId, uint newAmount)
        {
            id = newId;
            amount = newAmount;
        }

        [XmlAttribute("id")]
        public uint id = 0;
        [XmlAttribute("amount")]
        public uint amount = 0;
    }

    [XmlRoot("location")]
    public class PlayerLocation
    {
        [XmlAttribute]
        public uint realm = 0;
        [XmlAttribute]
        public uint location = 0;
    }

    public class UserAccount
    {
        public ulong userId = 0;
        [XmlElement]
        public PlayerLocation location = new PlayerLocation();
        public long balance = 0;
        public List<ItemEntry> inventory = new List<ItemEntry>();
    }
}
