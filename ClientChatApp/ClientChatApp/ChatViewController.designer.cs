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
	[Register ("ChatViewController")]
	partial class ChatViewController
	{
		[Outlet]
		AppKit.NSTextField ChatText { get; set; }

		[Outlet]
		AppKit.NSTextField TextToSend { get; set; }

		[Action ("SendText:")]
		partial void SendText (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (ChatText != null) {
				ChatText.Dispose ();
				ChatText = null;
			}

			if (TextToSend != null) {
				TextToSend.Dispose ();
				TextToSend = null;
			}
		}
	}
}
