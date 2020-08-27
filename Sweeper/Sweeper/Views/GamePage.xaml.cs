using Sweeper.Infrastructure;
using Sweeper.Models.Game;
using Sweeper.ViewModels;
using Sweeper.ViewModels.Converters;
using Sweeper.Views.Converters;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.ExceptionServices;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace Sweeper.Views
{
    public partial class GamePage : ContentPage
    {
        GamePageViewModel vm;
        Dictionary<string, Xamarin.Forms.ImageButton> buttonDictionary;
        static PieceValueToImageConverter converter = null;
        public GamePage()
        {
            InitializeComponent();
            if (converter == null)
            {
                converter = new PieceValueToImageConverter();
            }
            buttonDictionary = new Dictionary<string, Xamarin.Forms.ImageButton>();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            LayoutItems();   
        }

        bool firstPass = true;
        private void LayoutItems()
        {
            buttons.Children.Clear();
            Grid parent = (Grid)buttons.Parent;
            var bc = parent.BindingContext as GamePageViewModel;
            buttonDictionary.Clear();
            vm = bc;
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
            var cvt = new PieceValueToImageConverter();
            for (int i = 0; i < bc.Board.Rows; i++)
            {
                StackLayout row = new StackLayout() { Orientation = StackOrientation.Horizontal, Spacing = 2 };
                for (int j = 0; j < bc.Board.Columns; j++)
                {
                    var btn = new Xamarin.Forms.ImageButton() { Source = ImageSource.FromResource("Sweeper.Resources.button.png") };
                
                    btn.IsEnabled = false;
                    btn.BackgroundColor = Color.Blue;
                    btn.HeightRequest = itemHeight;
                    btn.WidthRequest = itemWidth;
                    btn.BindingContextChanged += Btn_BindingContextChanged;
                    btn.BindingContext = bc.Board[i, j];
                    
                    btn.CommandParameter = $"{i},{j}";
                   
                    //btn.Source = ImageSource.FromResource("Sweeper.Resources.button.png");
                    var bm = (GamePageViewModel)bc;
                    
                    
                   
                    
                    
                    btn.Aspect = Aspect.AspectFill;
                    
                    btn.Command = bc.PlayComand;
                    btn.IsEnabled = true;
                    
                    buttonDictionary.Add(bc.Board[i, j].Name, btn);
                   

                    row.Children.Add(btn);
                    //
                    bm.Board[i, j].PropertyChanged += GamePage_PropertyChanged;
                   
                }              
                buttons.Children.Add(row);
            }
            // firstPass = false;
            bc.Board.Refresh();
        }

        private void Btn_BindingContextChanged(object sender, EventArgs e)
        {
            var btn = sender as Xamarin.Forms.ImageButton;
            var gpm = btn.BindingContext as GamePieceModel;
            btn.Source = (ImageSource)converter.Convert(gpm.ShownValue, typeof(ImageSource), null, System.Globalization.CultureInfo.CurrentCulture);

        }

        private void GamePage_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            
            Sweeper.Models.Game.GamePieceModel gpm = sender as GamePieceModel;
            var n = gpm.Name;
            var btn = buttonDictionary[gpm.Name];
            btn.Source = (ImageSource)converter.Convert(gpm.ShownValue, typeof(ImageSource), null, System.Globalization.CultureInfo.CurrentCulture);   
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