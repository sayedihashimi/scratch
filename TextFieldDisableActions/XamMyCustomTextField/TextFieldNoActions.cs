using System;
using Foundation;
using ObjCRuntime;
using UIKit;

namespace XamMyCustomTextField {
    
    public partial class TextFieldNoActions : UIKit.UITextField {
        public override bool CanPerform(Selector action, NSObject withSender) {
            System.Diagnostics.Debug.WriteLine($"CanPerform:    \tname= {action.Name} handle= {action.Handle}");

            return false;
        }
    }
}
