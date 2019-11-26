using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using ServerChat;

class Program
{
    //Instancing objetcs
    static readonly object _lock = new object();
    //Creating a dictionnay to stock clients accordings to their id
    static readonly Dictionary<int, Client> list_clients = new Dictionary<int, Client>();

    //Here strats the main secction

    static void Main(string[] args)
    {
        int count = 1;

        //Initialize server sockets and listener waiting for client(s)
        TcpListener ServerSocket = new TcpListener(IPAddress.Any, 5000);
        ServerSocket.Start();

        //While loop for the server wait for clients over and over
        while (true)
        {
            Client client = new Client(ServerSocket.AcceptTcpClient(), null);
            //TcpClient client = ServerSocket.AcceptTcpClient();

            //Adding the lock to the client
            lock (_lock) list_clients.Add(count, client);
            Console.WriteLine("Client number : "+ count +" connected!!");

            //ChooseCorP();

            //New thread for the client connected
            Thread t = new Thread(handle_clients);
            t.Start(count);
            count++;
        }
    }

    public static void handle_clients(object o)
    {
        int id = (int)o;
        TcpClient client;

        lock (_lock) client = list_clients[id].client;

        StreamWriter sW = new StreamWriter(client.GetStream());
        sW.WriteLine(sendchannels());

        StreamReader sR = new StreamReader(client.GetStream());
        // Read the username (waiting for the client to use WriteLine())

        string channel = sR.ReadLine();
        Console.WriteLine(channel);
        list_clients[id].channel._name = channel;
        string username = "<" + sR.ReadLine().ToUpper() + ">";
        while (true)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int byte_count = stream.Read(buffer, 0, buffer.Length);

            if (byte_count == 0)
            {
                break;
            }

            string data = Encoding.ASCII.GetString(buffer, 0, byte_count);
            broadcast(data,username,channel);
            Console.WriteLine(username);
            Console.WriteLine(data);
        }

        lock (_lock) list_clients.Remove(id);
        client.Client.Shutdown(SocketShutdown.Both);
        client.Close();
    }

    //public static void ChooseCorP()
    //{
    //    Console.WriteLine("Do you want to connect a 1.person or a 2.Channel ?");
    //    string choice = Console.ReadLine();
    //    if (choice == "1")
    //    {
    //        foreach (var item in list_clients)
    //        {
    //            Console.WriteLine(item.Key+". "+item.Value);
    //        }
    //        {
    //            // do something with entry.Value or entry.Key
    //        }
    //    }

    //    else if (choice == "2")
    //    {

    //    }

    //}


    public static void broadcast(string data, string author, string commchan)
    {
        byte[] buffer = Encoding.ASCII.GetBytes(data + Environment.NewLine);
        byte[] bufferuser = Encoding.ASCII.GetBytes(author + Environment.NewLine);

        lock (_lock)
        {
            foreach (Client c in list_clients.Values)
            {
                string channel = c.channel._name;
                if (channel == commchan ) {
                    TcpClient cou = c.client;
                    NetworkStream stream = cou.GetStream();
                    stream.Write(bufferuser, 0, bufferuser.Length);
                    stream.Write(buffer, 0, buffer.Length);
                }
            }
        }
    }

    public static Channels sendchannels()
    {
        Channels channels = new Channels();
        channels.serialize(channels);
        return channels;

    }
}


//using System;
//using System.Collections.Generic;
//using System.Net;
//using System.Net.Sockets;
//using System.Text;
//using System.Threading;

//namespace ServerChat
//{
//    class Program
//    {

//        private static byte[] _buffer = new byte[1024];
//        private static List<Socket> _clientssocks = new List<Socket>();
//        private static Socket _serversocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);


//        public static void Main(string[] args)
//        {
//        SetupServer();
//        while (true)
//        {
//            Console.ReadLine();

//        }
//    }

//    private static void SetupServer()
//    {
//        Console.WriteLine("Setting up my server ...");
//        _serversocket.Bind(new IPEndPoint(IPAddress.Any, 100));
//        _serversocket.Listen(1);
//        _serversocket.BeginAccept(new AsyncCallback(AcceptCallBack), null);

//    }

//    private static void AcceptCallBack(IAsyncResult ASR)
//    {
//        Socket socket = _serversocket.EndAccept(ASR);
//        socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(RecieverCallBack), socket);
//        Console.WriteLine("Client connected");
//        _clientssocks.Add(socket);
//        _serversocket.BeginAccept(new AsyncCallback(AcceptCallBack), null);

//    }

//    private static void RecieverCallBack(IAsyncResult ASR)
//    {
//        Socket socket = (System.Net.Sockets.Socket)ASR.AsyncState;
//        int recieved = socket.EndReceive(ASR);
//        byte[] temp = new byte[recieved];
//        Array.Copy(_buffer, temp, recieved);
//        string message = Encoding.ASCII.GetString(temp);
//        Console.WriteLine("Message recieved : " + message);



//        byte[] temps = Encoding.ASCII.GetBytes(message);
//        socket.BeginSend(temps, 0, temps.Length, SocketFlags.None, new AsyncCallback(SendCallBack), socket);
//    }


//    private static void SendCallBack(IAsyncResult ASR)
//    {
//        Socket socket = (System.Net.Sockets.Socket)ASR.AsyncState;
//        socket.EndSend(ASR);
//    }
//}
//}
