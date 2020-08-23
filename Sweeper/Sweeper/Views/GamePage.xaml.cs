using Sweeper.ViewModels;
using System.Reflection;
using Xamarin.Forms;

namespace Sweeper.Views
{
    public partial class GamePage : ContentPage
    {
        public GamePage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            LayoutItems();   
        }

        private void LayoutItems()
        {
            buttons.Children.Clear();
            Grid parent = (Grid)buttons.Parent;
            var bc = parent.BindingContext as GamePageViewModel;
            double itemHeight, itemWidth;
            if (!isPortrait)
            {
                itemWidth = (parent.Width - ((bc.Board.Columns - 1) * bc.PieceSeparator)) / bc.Board.Columns;
                itemHeight = (parent.Height - ((bc.Board.Rows - 1) * bc.PieceSeparator)) / bc.Board.Rows;
            }else
            {
                itemWidth = (parent.Height - ((bc.Board.Columns - 1) * bc.PieceSeparator)) / bc.Board.Columns;
                itemHeight = (parent.Width - ((bc.Board.Rows - 1) * bc.PieceSeparator)) / bc.Board.Rows;
            }

            buttons.Spacing = 2;
            for (int i = 0; i < bc.Board.Rows; i++)
            {
                StackLayout row = new StackLayout() { Orientation = StackOrientation.Horizontal, Spacing = 2 };
                for (int j = 0; j < bc.Board.Columns; j++)
                {
                    var btn = new ImageButton() { };
                    btn.IsEnabled = false;
                    btn.BackgroundColor = Color.Blue;
                    btn.HeightRequest = itemHeight;
                    btn.WidthRequest = itemWidth;
                    btn.BindingContext = bc.Board[i, j];
                    btn.Source = ImageSource.FromResource("Sweeper.button.png");
                    
                    btn.Aspect = Aspect.AspectFill;
                    btn.CommandParameter = $"{i},{j}";
                    btn.Command = bc.PlayComand;
                    btn.IsEnabled = true;
                   
                    row.Children.Add(btn);
                }              
                buttons.Children.Add(row);
            }
        }

        private double width = 0;
        private double height = 0;
        private bool isPortrait = true;

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height); //must be called
            if (this.width != width || this.height != height)
            {
                this.width = width;
                this.height = height;
                isPortrait = width > height;
               // LayoutItems();       
                //reconfigure layout
            }
        }

        protected override void LayoutChildren(double x, double y, double width, double height)
        {
             base.LayoutChildren(x, y, width, height);
        }
        static GamePage()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            var lst = asm.GetManifestResourceNames();
        }
    }
}