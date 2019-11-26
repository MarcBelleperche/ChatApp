using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using ClientChat;

class Program
{
    static void Main(string[] args)
    {
        
        string name = login();

        IPAddress ip = IPAddress.Parse("127.0.0.1");
        int port = 5000;
        Console.Clear();
        TcpClient client = new TcpClient();
        client.Connect(ip, port);
        Console.WriteLine("client connected!!");
        NetworkStream ns = client.GetStream();
        StreamWriter sW = new StreamWriter(client.GetStream());
        sW.AutoFlush = true;

        StreamReader sR = new StreamReader(client.GetStream());

        Console.WriteLine("Select the channel on wich you want to communicate");
        
        string channel = Console.ReadLine();
        //Send the channel selected
        sW.WriteLine(channel);
        // Send the username
        sW.WriteLine(name);
        Thread thread = new Thread(o => ReceiveData((TcpClient)o));


        thread.Start(client);

        string s;
        while (!string.IsNullOrEmpty((s = Console.ReadLine())))
        {
            byte[] buffer = Encoding.ASCII.GetBytes(s);
            ns.Write(buffer, 0, buffer.Length);
        }

        client.Client.Shutdown(SocketShutdown.Send);
        thread.Join();
        ns.Close();
        client.Close();
        Console.WriteLine("disconnect from server!!");
        Console.ReadKey();
    }

    static void ReceiveData(TcpClient client)
    {
    NetworkStream ns = client.GetStream();
    byte[] receivedBytes = new byte[1024];
    int byte_count;

    while ((byte_count = ns.Read(receivedBytes, 0, receivedBytes.Length)) > 0)
        {
            Console.Write(Encoding.ASCII.GetString(receivedBytes, 0, byte_count));
        }
    }

    public static string login()
    {
        string use = "";
        Console.WriteLine("Do you want to login or register :");
        Console.WriteLine("1. Login");
        Console.WriteLine("2. Register");
        string value = "";
        while (value != "3")
        {
            value = Console.ReadLine();
            //LOGIN
            if (value == "1")
            {
                Clients c = Clients.deserialize();
                Console.WriteLine("Enter your username :");
                string username = Console.ReadLine();
                Console.WriteLine("Enter your password :");
                string password = Console.ReadLine();
                use = c.checkclients(username, password);
                if (use != null) value = "3";

            }

            //REGISTER
            else if (value == "2")
            {
                Console.WriteLine("Enter your name");
                string name = Console.ReadLine();
                Console.WriteLine("Enter your password");
                string psw = Console.ReadLine();

                Client client = new Client(name, psw);

                Clients clients = Clients.deserialize();
                clients._clients.Add(client);

                clients.serialize(clients);

            }
        }
        return use;
    }
}





//using System;
//using System.Net;
//using System.Net.Sockets;
//using System.Text;

//namespace ClientChat
//{
//    class Program
//    {
//        private static Socket _clientsocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

//        public static void Main(string[] args)
//        {
//            Connect();
//            SendLoop();
//            Console.ReadLine();

//        }

//        private static void SendLoop()
//        {
//            while (true)
//            {
//                Console.Write("Messsage to send : ");
//                string message = Console.ReadLine();
//                byte[] temp = Encoding.ASCII.GetBytes(message);
//                _clientsocket.Send(temp);

//                byte[] temps = new byte[2046];

//                int rec = _clientsocket.Receive(temps);
//                byte[] messagerecieved = new byte[rec];
//                Array.Copy(temps, messagerecieved, rec);
//                string reception = Encoding.ASCII.GetString(messagerecieved);
//                Console.WriteLine("Message Sended : " + reception);


//            }
//        }

//        private static void Connect()
//        {
//            while (!_clientsocket.Connected)
//            {
//                try
//                {
//                    _clientsocket.Connect(IPAddress.Loopback, 100);

//                }
//                catch (SocketException)
//                {
//                    Console.WriteLine("Impossible to connect server");
//                }

//            }
//        }
//    }
//}

