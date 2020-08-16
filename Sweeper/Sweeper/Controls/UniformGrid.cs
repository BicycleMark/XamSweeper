using System;
using System.Collections;
using System.Windows.Input;
using Xamarin.Forms;

namespace Sweeper.Controls
{
    public class UniformGrid : Frame, IDisposable
    {
        public int ItemHeight { get; private set; }
        public int ItemWidth { get; private set; }

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create("ItemsSource", typeof(IList), typeof(UniformGrid), null, BindingMode.TwoWay);
         public IList ItemsSource
        {
            get { return (IList)GetValue(ItemsSourceProperty); }
            set { this.SetValue(ItemsSourceProperty, value); }
        }

        public static readonly BindableProperty ItemTemplateProperty =
          BindableProperty.Create("ItemTemplate", typeof(DataTemplate), typeof(UniformGrid), null, BindingMode.TwoWay);
        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { this.SetValue(ItemTemplateProperty, value); }
        }

        public static readonly BindableProperty RowsProperty =
            BindableProperty.Create("Rows", typeof(int), typeof(UniformGrid), 2, BindingMode.TwoWay);

        public int Rows
        {
            get { return (int)GetValue(RowsProperty); }
            set { this.SetValue(RowsProperty, value); }
        }

              
        public static readonly BindableProperty ForegroundBrushProperty =
           BindableProperty.Create("ForegroundBrush", typeof(Brush), typeof(UniformGrid), null, BindingMode.TwoWay);

        public int ForegroundBrush
        {
            get { return (int)GetValue(ForegroundBrushProperty); }
            set { this.SetValue(ForegroundBrushProperty, value); }
        }

        public static readonly BindableProperty BackgroundBrushProperty =
           BindableProperty.Create("BackgroundBrush", typeof(Brush), typeof(UniformGrid), null, BindingMode.TwoWay);

        public int BackgroundBrush
        {
            get { return (int)GetValue(ForegroundBrushProperty); }
            set { this.SetValue(ForegroundBrushProperty, value); }
        }

        public static readonly BindableProperty ItemTappedCommandProperty =
          BindableProperty.Create(nameof(ItemTappedCommand), typeof(ICommand), typeof(UniformGrid), null, BindingMode.TwoWay);

        public int ItemTappedCommand
        {
            get { return (int)GetValue(ItemTappedCommandProperty); }
            set { this.SetValue(ItemTappedCommandProperty, value); }
        }

        public UniformGrid()
        {
            this.ItemHeight = this.ItemHeight = 40;       
        }

        public void Dispose()
        {          
            //throw new NotImplementedException();
        }
    }
}
