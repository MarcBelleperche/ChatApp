using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using AppKit;

namespace ClientChatApp
{
    [Serializable]
    public class Clients
    {
        public List<Client> _clients;

        public Clients()
        {
            this._clients = new List<Client>();
            _clients.Add(new Client("Fiona", "youyou"));
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

        public string checkclients(string username, string psw, NSTextField print)
        {
            string name = null;
            int find = 0;
            foreach (Client clients in _clients)
            {
                if (clients._name == username)
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
                else
                {
                    find = 3;
                }
            }

            if (find == 1)
            {
                print.TextColor = NSColor.White;
                //Console.WriteLine("Your connected");
                print.StringValue = "You are connected";
            }
            else if (find == 2)
            {
                print.TextColor = NSColor.Red;
                //Console.WriteLine("Wrong password !!!");
                print.StringValue = "Wrong password !";

            }
            else if (find == 3)
            {
                print.TextColor = NSColor.Red;
                //Console.WriteLine("You're not registed !!!");
                print.StringValue = "You are not registered !";
            }

            return name;
        }


    }
}
