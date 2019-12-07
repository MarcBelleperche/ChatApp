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
	[Register ("SecondViewController")]
	partial class SecondViewController
	{
		[Outlet]
		AppKit.NSSecureTextField password { get; set; }

		[Outlet]
		AppKit.NSSecureTextField passwordcheck { get; set; }

		[Outlet]
		AppKit.NSTextField username { get; set; }

		[Action ("Register:")]
		partial void Register (AppKit.NSButton sender);

		[Action ("Regiterr:")]
		partial void Regiterr (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (password != null) {
				password.Dispose ();
				password = null;
			}

			if (passwordcheck != null) {
				passwordcheck.Dispose ();
				passwordcheck = null;
			}

			if (username != null) {
				username.Dispose ();
				username = null;
			}
		}
	}
}
