using System;
using System.Net.Sockets;

namespace ServerChat
{
    public class Client
    {
        public TcpClient client;
        public Channel channel;

        public Client(TcpClient client, Channel channel)
        {
            this.channel = channel;
            this.client = client;
        }
    }
}
