using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ClientChat
{
    [Serializable]
    public class Clients
    {
        public List<Client> _clients;

        public Clients()
        {
            this._clients = new List<Client>();
            _clients.Add(new Client("Fiona","youyou"));
        }

        public void serialize(Clients clients)
        {
            IFormatter formatter = new BinaryFormatter();
            //We create a new file called "Clients.bin"
            FileStream stream = new FileStream("Clients.bin", FileMode.Create, FileAccess.Write);
            formatter.Serialize(stream, clients);
            stream.Close();
        }

        public static Clients deserialize()
        {
            IFormatter formatter = new BinaryFormatter();
            //We call the .bin file 
            FileStream stream = new FileStream("Clients.bin", FileMode.Open, FileAccess.Read);
            Clients c = new Clients();
            c = (Clients)formatter.Deserialize(stream);
            stream.Close();
            return c;
        }

        public string checkclients(string username, string psw)
        {
            string name = null;
            int find = 0;
            foreach (Client clients in _clients )
            {
                if(clients._name == username)
                {
                    if (clients._password == psw)
                    {
                        find = 1;
                        name = clients._name;
                        break;
                    }
                    else
                    {
                        find = 2;
                    }
                }
                else{
                    find = 3;
                }
            }

            if(find == 1)
            {
                Console.WriteLine("Your connected");
            }
            else if (find == 2)
            {
                Console.WriteLine("Wrong password !!!");

            }
            else if (find == 3)
            {
                Console.WriteLine("You're not registed !!!");
            }

            return name;
        }

       
    }
}
