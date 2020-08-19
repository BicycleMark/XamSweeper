using System;
using System.Collections;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;

namespace Sweeper.Controls
{
    public class UniformGrid : Frame, IDisposable
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
            set { this.SetValue(RowsProperty, value); }
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
            set { this.SetValue(ColumnsProperty, value); }
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
            //return (new SizeRequest(new Size(widthConstraint, heightConstraint)));          
        }



        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

        }

        protected override void InvalidateLayout()
        {
            var rows = Rows;
            var cols = Columns;
            
            foreach (var ch in Children)
            {
                
            }
            for (int r = 0; r < rows; r++)
            {

            }
            
            base.InvalidateLayout();
        }

        int _rows;
        int _columns;
        private void UpdateComputedValues()
        {
            _columns = Columns;
            _rows = Rows;

            //parameter checking. 
           
            if ((_rows == 0) || (_columns == 0))
            {
                int nonCollapsedCount = 1;

                if (_rows == 0)
                {
                    if (_columns > 0)
                    {
                        // take FirstColumn into account, because it should really affect the result
                        _rows = (nonCollapsedCount + 0 /*FirstColumn*/ + (_columns - 1)) / _columns;
                    }
                    else
                    {
                        // both rows and columns are unset -- lay out in a square
                        _rows = (int)Math.Sqrt(nonCollapsedCount);
                        if ((_rows * _rows) < nonCollapsedCount)
                        {
                            _rows++;
                        }
                        _columns = _rows;
                    }
                }
                else if (_columns == 0)
                {
                    // guaranteed that _rows is not 0, because we're in the else clause of the check for _rows == 0
                    _columns = (nonCollapsedCount + (_rows - 1)) / _rows;
                }
            }
        }
        public void Dispose()
        {          
            //throw new NotImplementedException();
        }
    }
}
