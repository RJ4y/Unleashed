using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using UnleashedApp.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(CustomTabBarNoIconRenderer))]
namespace UnleashedApp.iOS.Renderers
{
    public class CustomTabBarNoIconRenderer: TabbedRenderer
    {
        public override void ViewWillAppear(bool animated)
        {
            if (TabBar?.Items == null)
                return;

            // Go through our elements and change them
            var tabs = Element as TabbedPage;
            if (tabs != null)
            {
                for (int i = 0; i < TabBar.Items.Length; i++)
                    UpdateTabBarItem(TabBar.Items[i]);
            }

            base.ViewWillAppear(animated);
        }

        private void UpdateTabBarItem(UITabBarItem item)
        {
            if (item == null)
                return;

            //Font size 
            item.SetTitleTextAttributes(new UITextAttributes() { Font = UIFont.FromName("Helvetica Neue", 15F) }, UIControlState.Normal);
            item.SetTitleTextAttributes(new UITextAttributes() {Font = UIFont.FromName("Helvetica Neue", 20F) }, UIControlState.Selected);

            // Moves the titles up just a bit.
            item.TitlePositionAdjustment = new UIOffset(0, -15);
        }
    }
}