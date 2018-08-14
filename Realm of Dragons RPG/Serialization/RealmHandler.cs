using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

using Realm_of_Dragons_RPG;

namespace Realm_of_Dragons_RPG.Serialization
{
    [XmlRoot("connection")]
    public class Connection
    {
        [XmlAttribute("realm")]
        public uint realm = 0;
        [XmlAttribute("location")]
        public uint location = 0;
        [XmlAttribute("cost")]
        public int cost = 0;
    }
    [XmlRoot("Location")]
    public class Location
    {
        [XmlAttribute]
        public string name = "Unnamed Location";
        [XmlArray]
        public List<Connection> connections = new List<Connection>();
    }

    [XmlRoot("Realm")]
    public class Realm
    {
        [XmlAttribute]
        public string name = "Unnamed Realm";
        [XmlArray]
        public List<Location> locations = new List<Location>();
    }

    [XmlRoot("WorldMap")]
    public class WorldMap
    {
        [XmlArray]
        public List<Realm> realms = new List<Realm>();
    }

    public static class RealmHandler
    {
        static RealmHandler()
        {
            worldMap = Serialization.Serializer.loadWorld();
        }

        public static WorldMap worldMap = new WorldMap();
    }
}
