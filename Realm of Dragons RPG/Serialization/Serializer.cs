using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Realm_of_Dragons_RPG.Serialization
{
    public static class Serializer
    {

        public static UserAccount loadUser(ulong id)
        {
            UserAccount result = new UserAccount();

            string filename = "../../players/" + id.ToString() + ".xml";

            if (!File.Exists(filename))
            {
                return result;
            }

            XmlSerializer ser = new XmlSerializer(typeof(UserAccount));

            FileStream fs = new FileStream(filename, FileMode.Open);

            result = (UserAccount)ser.Deserialize(fs);

            fs.Close();

            return result;
        }

        public static void saveUser(UserAccount user)
        {
            string filename = "../../players/" + user.userId.ToString() + ".xml";
            XmlSerializer ser = new XmlSerializer(typeof(UserAccount));
            TextWriter writer = new StreamWriter(filename);
            ser.Serialize(writer, user);
            writer.Close();

            return;
        }

        public static WorldMap loadWorld()
        {
            WorldMap map = new WorldMap();
            string filename = "../../world/map.xml";

            if (!File.Exists(filename))
            {
                return map;
            }

            XmlSerializer ser = new XmlSerializer(typeof(WorldMap));

            FileStream fs = new FileStream(filename, FileMode.Open);

            map = (WorldMap)ser.Deserialize(fs);

            fs.Close();

            return map;
        }

        public static void saveWorld(WorldMap map)
        {
            string filename = "../../world/map.xml";
            XmlSerializer ser = new XmlSerializer(typeof(WorldMap));
            TextWriter writer = new StreamWriter(filename);
            ser.Serialize(writer, map);
            writer.Close();

            return;
        }

        public static ItemCollection loadCollection(string collection_name)
        {
            ItemCollection collection = new ItemCollection();
            string path = "../../items/" + collection_name + ".xml";

            if (!File.Exists(path))
            {
                return collection;
            }

            XmlSerializer ser = new XmlSerializer(typeof(ItemCollection));

            FileStream fs = new FileStream(path, FileMode.Open);

            collection = (ItemCollection)ser.Deserialize(fs);

            fs.Close();

            return collection;
        }

        public static void saveCollection(ItemCollection collection, string collection_name)
        {
            string path = "../../items/" + collection_name + ".xml";
            XmlSerializer ser = new XmlSerializer(typeof(ItemCollection));
            TextWriter writer = new StreamWriter(path);
            ser.Serialize(writer, collection);
            writer.Close();

            return;
        }
    }
}
