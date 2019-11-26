using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using AppKit;
using Foundation;

namespace ClientChatApp
{
    public partial class ViewController : NSViewController
    {
        public ViewController(IntPtr handle) : base(handle)
        {
      
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            
            //Console.ReadKey();

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
        partial void login(NSObject sender)
        {
            string name = Login();
            if (name != null)
            {
                IPAddress ip = IPAddress.Parse("127.0.0.1");
                int port = 5000;
                //Console.Clear();

                using (TcpClient client = new TcpClient())
                {
                    try
                    {
                        client.Connect(ip, port);
                        Console.WriteLine("client connected!!");
                        ChatViewController chatView = new ChatViewController(client, name);
                    }
                    catch (SocketException)
                    {
                        wrongpwd.StringValue = "Please lauch the server";
                    }


                }
                //Console.WriteLine("disconnect from server!!");
                //Clients addlcient = new Clients();
                //addlcient.serialize(addlcient);
                //wrongpwd.StringValue = "Wrong password or user name !";
            }


        }

        public string Login()
        {
            //addlcient._clients.Add(new Client(username.StringValue, null));
            //Clients check = new Clients();
            //Clients list = check.deserialize();
            string use = "";
            Clients c = Clients.deserialize();
            //Console.WriteLine("Enter your username :");
            string user_name = username.StringValue;
            //Console.WriteLine("Enter your password :");
            string pass_word = password.StringValue;
            use = c.checkclients(user_name, pass_word, wrongpwd);
            //if (use == null) wrongpwd.StringValue = "Wrong password or not registered";
            return use;

        }

        //public void Register()
        //{

        //}

       
    }
}
