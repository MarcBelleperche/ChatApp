// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ClientChatApp
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		AppKit.NSView loginpage { get; set; }

		[Outlet]
		AppKit.NSSecureTextField password { get; set; }

		[Outlet]
		AppKit.NSTextField username { get; set; }

		[Outlet]
		AppKit.NSTextField wrongpwd { get; set; }

		[Action ("login:")]
		partial void login (Foundation.NSObject sender);

		[Action ("register:")]
		partial void register (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (loginpage != null) {
				loginpage.Dispose ();
				loginpage = null;
			}

			if (password != null) {
				password.Dispose ();
				password = null;
			}

			if (username != null) {
				username.Dispose ();
				username = null;
			}

			if (wrongpwd != null) {
				wrongpwd.Dispose ();
				wrongpwd = null;
			}
		}
	}
}
