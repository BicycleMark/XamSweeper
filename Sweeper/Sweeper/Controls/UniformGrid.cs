using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;

namespace Sweeper.Controls
{
    public class UniformGrid : Layout<View>, IDisposable
    {
        public double ItemHeight { get; private set; }
        public double ItemWidth { get; private set; }

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
             BindableProperty.Create(propertyName: nameof(Rows),
                                     returnType: typeof(int),
                                     declaringType: typeof(UniformGrid),
                                     defaultValue: 2,
                                     defaultBindingMode: BindingMode.TwoWay,
                                     propertyChanged: (bindable, oldvalue, newvalue) =>
                                     {
                                        ((UniformGrid)bindable).InvalidateLayout();
                                     });

        public int Rows
        {
            get { return (int)GetValue(RowsProperty); }
            set { 
                this.SetValue(RowsProperty, value); 
            }
        }

        public static readonly BindableProperty ColumnsProperty =
            
            BindableProperty.Create(propertyName:nameof(Columns),
                                    returnType: typeof(int),
                                    declaringType: typeof(UniformGrid),
                                    defaultValue: 2,
                                    defaultBindingMode: BindingMode.TwoWay,
                                    propertyChanged: (bindable, oldvalue, newvalue) =>
                                    {
                                        ((UniformGrid)bindable).InvalidateLayout();
                                    });


        public int Columns
        {
            get { return (int)GetValue(ColumnsProperty); }
            set { 
                this.SetValue(ColumnsProperty, value); 
            }
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

        public UniformGrid():base()
        {
            
           
        }

        bool isFirstTime = true;
        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {


            if (isFirstTime)
            {
                ItemHeight = this.Height / this.Rows;
                ItemWidth = this.Width / this.Columns;
            }
            isFirstTime = false;
            return base.OnMeasure(widthConstraint, heightConstraint);   
        }
        
        protected override void InvalidateLayout()
        {
            List<StackLayout> lst = new List<StackLayout>();
            var rows = Rows;
            var cols = Columns;
            if (rows != 0 && cols != 0)
            {
                Children.Clear();

                StackLayout rl = new StackLayout() { Orientation = StackOrientation.Vertical };
                for (int r = 0; r < rows; r++)
                {
                    StackLayout sl = new StackLayout() { Orientation = StackOrientation.Horizontal };
                    for (int c = 0; c < cols; c++)
                    {
                        sl.Children.Add(new Button() { WidthRequest = ItemWidth, HeightRequest = ItemHeight, Text = "Hello" });
                    }
                    rl.Children.Add(sl);
                }
                Children.Add(rl);
            }
           // base.InvalidateLayout();
        }

        int _rows;
        int _columns;
      
        public void Dispose()
        {          
            //throw new NotImplementedException();
        }

        protected override void LayoutChildren(double x, double y, double width, double height) //: base(x, y, width, height)
        {
            InvalidateLayout();

        }
    }
}
