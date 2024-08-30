using Microsoft.Maui;
using System.Drawing;
#if IOS
using UIKit;
using Foundation;
#endif
namespace MySARAssist.Models
{
    public class EntryHandler
    {
        public static void AddDone()
        {
            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("Done", (handler, view) =>
            {
#if IOS
            var toolbar = new UIToolbar(new RectangleF(0.0f, 0.0f, 50.0f, 44.0f));
            toolbar.BackgroundColor = UIColor.LightGray; // Set the color you prefer
            var doneButton = new UIBarButtonItem(UIBarButtonSystemItem.Done, delegate
            {
                handler.PlatformView.ResignFirstResponder();
            });

            toolbar.Items = new UIBarButtonItem[] {
                new UIBarButtonItem (UIBarButtonSystemItem.FlexibleSpace),
                doneButton
            };

            handler.PlatformView.InputAccessoryView = toolbar;
#endif
            });
        }
    }
}
