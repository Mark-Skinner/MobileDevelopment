// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace WillowTree.NameGame.iOS
{
	[Register ("MainView")]
	partial class MainView
	{
		[Outlet]
		UIKit.UIStackView[] innerStacks { get; set; }

		[Outlet]
		UIKit.UIStackView outerStack { get; set; }

		[Outlet]
		WillowTree.NameGame.iOS.PersonCell[] personCells { get; set; }

		[Outlet]
		UIKit.UILabel questionLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (questionLabel != null) {
				questionLabel.Dispose ();
				questionLabel = null;
			}

			if (outerStack != null) {
				outerStack.Dispose ();
				outerStack = null;
			}
		}
	}
}
