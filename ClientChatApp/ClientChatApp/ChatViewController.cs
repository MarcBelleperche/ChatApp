using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Text;

namespace ClientChatApp
{
    public partial class ChatViewController : NSViewController
    {
        TcpClient client;
        string name;
        // Called when created from unmanaged code
        public ChatViewController(IntPtr handle) : base(handle)
        {
        }

        public ChatViewController(TcpClient client, string name)
        {
            this.name = name;
            this.client = client;
        }

        // Called when created directly from a XIB file
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            NetworkStream ns = client.GetStream();
            StreamWriter sW = new StreamWriter(client.GetStream());
            sW.AutoFlush = true;

            //StreamReader sR = new StreamReader(client.GetStream());

            //Console.WriteLine("Select the channel on wich you want to communicate");

            //string channel = Console.ReadLine();
            //Send the channel selected
            //sW.WriteLine(channel);
            // Send the username

            sW.WriteLine(name);
            Thread thread = new Thread(o => ReceiveData((TcpClient)o));


            thread.Start(client);

            string s;
            while (!string.IsNullOrEmpty((s = Console.ReadLine())))
            {
                byte[] buffer = Encoding.ASCII.GetBytes(s);
                ns.Write(buffer, 0, size: buffer.Length);
            }

            client.Client.Shutdown(SocketShutdown.Send);
            thread.Join();
            ns.Close();
            client.Close();
            // Do any additional setup after loading the view.
        }

        public override NSObject RepresentedObject
        {
            get
            {
                return base.RepresentedObject;
            }
            set
            {
                base.RepresentedObject = value;
                // Update the view, if already loaded.
            }
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
    }
   
}
