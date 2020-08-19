using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace Sweeper.Controls
{
    public class GridOverlayLayout : AbsoluteLayout
    {
        
    }

    public class GridLayout : RelativeLayout
    {
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
        }
        protected override void OnPropertyChanging([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanging(propertyName);
        }
    }

    public class MyLayout : Layout
    {
        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            throw new NotImplementedException();
        }
    }
}
