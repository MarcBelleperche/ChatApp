using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;
using System.Net.Sockets;

namespace ClientChatApp
{
    public partial class SecondViewController : NSViewController
    {
        // Called when created from unmanaged code
        public SecondViewController(IntPtr handle) : base(handle)
        {
        }

        // Called when created directly from a XIB file
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

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

        partial void Regiterr(Foundation.NSObject sender)
        {
            //Console.WriteLine("Enter your name");
            //string name = Console.ReadLine();
            string name = username.StringValue;
            //Console.WriteLine("Enter your password");
            //string psw = Console.ReadLine();
            string psw = password.StringValue;
            string pswconfirm = passwordcheck.StringValue;

            if (psw == pswconfirm)
            {
                Client client = new Client(name, psw);

                Clients clients = Clients.deserialize();
                clients._clients.Add(client);

                clients.serialize(clients);
            }
            View.Dispose();
            //if (password.StringValue == passwordcheck.StringValue) {
            //    Clients addlcient = new Clients();
            //    addlcient._clients.Add(new Client(username.StringValue, null));
            //    addlcient.serialize(addlcient);
            //}

        }

    }
}
