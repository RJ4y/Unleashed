using System;
using System.Collections.Generic;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
namespace UnleashedApp.iOS.Renderers
{
    public class CustomNavigationBarRenderer : NavigationRenderer
    {

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            NavigationBar.TopItem.TitleView = new UIImageView(UIImage.FromBundle("unleashed.png"))
            {
                ContentMode = UIViewContentMode.ScaleAspectFit
            };


        }

    }
}
