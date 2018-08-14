using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Realm_of_Dragons_RPG;

namespace Realm_of_Dragons_RPG.Data
{
    public static class Users
    {
        static Users()
        {
            string[] fileEntries = Directory.GetFiles("../../players/");
            foreach (string fileName in fileEntries)
            {
                Console.WriteLine(fileName);
                ulong id = ulong.Parse(Path.GetFileNameWithoutExtension(fileName));
                Serialization.UserAccount user = Serialization.Serializer.loadUser(id);
                if (user.userId != 0)
                {
                    users.Add(user.userId, user);
                }
            }
        }
        public static Dictionary<ulong, Serialization.UserAccount> users = new Dictionary<ulong, Serialization.UserAccount>();
        public static void refreshAll()
        {
            string[] fileEntries = Directory.GetFiles("../../players/");
            foreach (string fileName in fileEntries)
            {
                Console.WriteLine(fileName);
                Serialization.UserAccount user = Serialization.Serializer.loadUser(ulong.Parse(fileName));
                if (user.userId != 0)
                {
                    users.Add(user.userId, user);
                }
            }
        }
        public static void refresh(ulong id)
        {
            if(users.ContainsKey(id))
                users[id] = Serialization.Serializer.loadUser(id);
        }
        public static void saveAll()
        {
            foreach(KeyValuePair<ulong, Serialization.UserAccount> user in users)
            {
                Serialization.Serializer.saveUser(user.Value);
            }
        }
        public static void save(ulong id)
        {
            Serialization.Serializer.saveUser(users[id]);
        }
    }
}
