using System;
using System.Net.Sockets;

namespace ClientChatApp
{
    [Serializable]
    public class Client
    {
        public string _name;
        public string _password;
        public string _channel;

        public Client(string name, string psw)
        {
            this._name = name;
            this._password = psw;
        }
    }
}
